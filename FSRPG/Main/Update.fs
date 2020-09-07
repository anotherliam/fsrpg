namespace FSRPG.Main

module Update =
    open Microsoft.Xna.Framework
    open FSRPG
    open FSRPG.Input

    let moveCamera (inputState: Input.InputState) delta controlState =
        // some garbage code to move the camera
        if (inputState.TertiaryMouse.Down)
            then
                if (inputState.MouseMoved)
                    then
                        let xDelta = inputState.MouseXDelta / controlState.Camera.Zoom
                        let yDelta = inputState.MouseYDelta / controlState.Camera.Zoom
                        {
                            controlState with
                                Camera =
                                    controlState.Camera
                                    |> Camera.pan ((new Vector2 (-xDelta, -yDelta)))
                        }
                    else controlState
            else
                let x = float32 ((if inputState.ButtonRight.Down then Config.MapSpeed else 0) + (if inputState.ButtonLeft.Down then -Config.MapSpeed else 0))
                let y = float32 ((if inputState.ButtonDown.Down then Config.MapSpeed else 0) + (if inputState.ButtonUp.Down then -Config.MapSpeed else 0))
                {
                    controlState with
                        Camera = Camera.pan ((new Vector2 (x * delta, y * delta))) controlState.Camera
                }

    
    let updateHighlightedTile controlState pos =
        {
            controlState with
                HighlightedTile = Some pos
        }

    let maybeSelectActor (actor: Game.Actor) (player: int8) = if actor.Team = player then Actions.SelectActor (actor) else Actions.NoOp

    let tick (graphics: GraphicsDeviceManager) (gameTime: GameTime) (gameState: GameState) =
            let delta = float32 gameTime.ElapsedGameTime.TotalSeconds
            match gameState with
            | Loading ->
                // Create all the basic game state and shit
                let gameState = State.WorldStateUtils.createGenericGameState ()
                let viewport = graphics.GraphicsDevice.Viewport;
                // Create a camera and center it on the middle of the map
                let camera =
                    Camera.create viewport
                    |> Camera.centerOn (new Vector2 (float32 (gameState.TileMap.PixelWidth / 2), float32(gameState.TileMap.PixelHeight / 2)))
                let controlState = { Camera = camera; HighlightedTile = None }
                // Create the gamestate manager
                let manager = StateManager.StateManager.create gameState [ Reducer.reduce ]
                // Nice, lets play the game or something dude
                Playing {
                        WorldStateManager = manager
                        InputState = Input.InputUtils.getInput None
                        ControlState = controlState
                }
            | Playing { WorldStateManager = worldStateManager; InputState = inputState; ControlState = controlState; } ->
                // Generate new inputs, and find the world position
                let worldState = worldStateManager.Current
                let input = Input.InputUtils.getInput (Some inputState)
                let mouseWorldPos = controlState.Camera.ScreenToWorld (new Vector2 (input.MouseX, input.MouseY))
                let newControlState =
                    controlState
                    |> moveCamera input delta

                // Get input events
                let inputEvent = InputEvent.handleMapInput worldState.TileMap input mouseWorldPos
                    
                do System.Diagnostics.Debug.WriteLine (sprintf "state type is %A" worldState.Type)

                // Do actual game logic
                let (newStateManager, newControlState) =
                    match worldState.Type with
                    | State.Empty -> (worldStateManager, newControlState)
                    | State.Standby ->
                        match inputEvent with
                        // We clicked
                        | InputEvent.MapTile tile when input.PrimaryMouse.Press ->
                            let ucs = updateHighlightedTile newControlState tile
                            match Game.WorldUtils.getActorOnTile worldState.Actors tile with
                            | Some actor -> ( (worldStateManager.next (maybeSelectActor actor worldState.PlayersTurn)), ucs )
                            | None -> ( worldStateManager, ucs )
                        // Right click
                        | InputEvent.MapTile tile when input.SecondaryMouse.Press ->
                            (
                                worldStateManager.next Actions.UnselectActor,
                                updateHighlightedTile newControlState tile
                            )
                        | InputEvent.Empty when input.SecondaryMouse.Press ->
                            (
                                worldStateManager.next Actions.UnselectActor,
                                newControlState
                            )
                        // No click, but mouse move
                        | InputEvent.MapTile tile when input.MouseMoved ->
                            ( worldStateManager, (updateHighlightedTile newControlState tile) )
                        | _ -> ( worldStateManager, newControlState )
                    | State.SelectedActor selectedActor ->
                        match inputEvent with
                        // Left clicked
                        | InputEvent.MapTile tile when input.PrimaryMouse.Press ->
                            let ucs = updateHighlightedTile newControlState tile
                            match Game.WorldUtils.getActorOnTile worldState.Actors tile with
                            // Clicked on an actor, select it if possible
                            | Some actor -> ( (worldStateManager.next (maybeSelectActor actor worldState.PlayersTurn)), ucs )
                            // Clicked on an empty square, move there (TODO: Check if its a valid move lol)
                            | None -> ( (worldStateManager.next (Actions.MoveActorTo (selectedActor, tile))), ucs )
                        // Right click, just unselect the actor
                        | InputEvent.MapTile tile when input.SecondaryMouse.Press ->
                            (
                                worldStateManager.next Actions.UnselectActor,
                                updateHighlightedTile newControlState tile
                            )
                        | InputEvent.Empty when input.SecondaryMouse.Press ->
                            (
                                worldStateManager.next Actions.UnselectActor,
                                newControlState
                            )
                        // No click, but mouse move
                        | InputEvent.MapTile tile when input.MouseMoved ->
                            ( worldStateManager, (updateHighlightedTile newControlState tile) )
                        | _ -> ( worldStateManager, newControlState )
                // Continue
                Playing {
                        WorldStateManager = newStateManager
                        InputState = input
                        ControlState = newControlState
                }
