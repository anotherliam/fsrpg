namespace FSRPG

module Game =

    open Microsoft.Xna.Framework
    open Microsoft.Xna.Framework.Graphics
    open Microsoft.Xna.Framework.Content
    open Microsoft.Xna.Framework.Input
    open MapHelpers
    open System


    type TransitionType<'t> =
        | Next of 't
        | Prev of int
        | Stay
    
    // Control state contains camera positions and highlighted tile information
    // This is seperate from gamestate as it doesnt trigger transitions and isnt saved
    type ControlState = {
        Camera: Camera;
        HighlightedTile: Option<Point>;
    }
        
    type OuterState =
        | Loading
        | Playing of GameState * Input.InputState * ControlState


    type Game1 () as this =
        inherit Game()
 
        do this.Content.RootDirectory <- "Content"
        do testing.runEvent testing.someEvent {values = 5}
        do testing.runWordsTest ()
        let graphics = new GraphicsDeviceManager(this)
        let mutable spriteBatch = Unchecked.defaultof<SpriteBatch>

        let mutable state = Loading
        let mutable engineState: Option<EngineState> = None

        // For fps
        let mutable prevElapsedSeconds = 0.0

        let textColor = Color.DarkSlateBlue

        let moveCamera (inputState: Input.InputState) delta controlState =
            // some garbage code to move the camera
            if (inputState.TertiaryMouse.Down)
                then
                    if (inputState.MouseMoved)
                        then
                            let xDelta = inputState.MouseXDelta
                            let yDelta = inputState.MouseYDelta
                            {
                                controlState with
                                    Camera = controlState.Camera.pan ((new Vector2 (float32 xDelta, float32 yDelta)))
                            }
                        else controlState
                else
                    let x = float32 ((if inputState.ButtonLeft.Down then Config.MapSpeed else 0) + (if inputState.ButtonRight.Down then -Config.MapSpeed else 0))
                    let y = float32 ((if inputState.ButtonUp.Down then Config.MapSpeed else 0) + (if inputState.ButtonDown.Down then -Config.MapSpeed else 0))
                    {
                        controlState with
                            Camera = controlState.Camera.pan ((new Vector2 (x * delta, y * delta)))
                    }

        let findHighlightedTile (gameState: GameState) (inputState: Input.InputState) (controlState: ControlState) =
            // Only if the mouse position is within the map
            let mapSize = new Rectangle (0, 0, gameState.TileMap.PixelWidth, gameState.TileMap.PixelHeight)
            let adjustedMouse = new Vector2 ((float32 inputState.MouseX) - controlState.Camera.X, (float32 inputState.MouseY) - controlState.Camera.Y)
            if mapSize.Contains (adjustedMouse)
                then 
                    let x = Math.Floor((float adjustedMouse.X) / (float gameState.TileMap.Tileset.TileWidth))
                    let y = Math.Floor((float adjustedMouse.Y ) / (float gameState.TileMap.Tileset.TileHeight))
                    {
                    controlState with
                        HighlightedTile = Some (new Point (int x, int y))
                    }
                else controlState

        // Functions for drawing i guess

        let drawMap (tmap: MapHelpers.ActiveTilemap) (time: float) (pos: Vector2) =
            let getFrame (anim: List<int * int>) =
                let t = time % ((float anim.Length) * 0.5)
                anim.[int (floor (t / 0.5))]
            let w = tmap.Tileset.TileWidth
            let h = tmap.Tileset.TileHeight
            let rec drawTile: (List<MapHelpers.Tile> -> unit) = function
                | tile::tail ->
                    let destX = float (tile.X * w) + (float pos.X)
                    let destY = float (tile.Y * h) + (float pos.Y)
                    let dest = new Vector2 ((Math.Floor destX) |> float32, (Math.Floor destY) |> float32)
                    tile.Layers
                    |> List.iter ( function
                        | MapHelpers.Animated anim ->
                            let (x, y) = getFrame anim
                            let src = new System.Nullable<Rectangle>(new Rectangle(x, y, w, h))
                            do spriteBatch.Draw (tmap.Texture, dest, src, Color.White)
                        | MapHelpers.Static (x, y) ->
                            let src = new System.Nullable<Rectangle>(new Rectangle(x, y, w, h))
                            do spriteBatch.Draw (tmap.Texture, dest, src, Color.White)
                    )
                    drawTile tail
                | [] -> ()
            drawTile tmap.Tiles
            ()

        let drawHighlightedTile (tmap: MapHelpers.ActiveTilemap) (tile: Point) (camera: Camera) =
            let tWidth = int tmap.Tileset.TileWidth
            let tHeight = int tmap.Tileset.TileHeight
            let dest = new Rectangle((tile.X * tWidth + (int camera.X)), (tile.Y * tHeight + (int camera.Y)), 16, 16)
            let src = Nullable(new Rectangle (0, 0, 32, 32))
            do spriteBatch.Draw (Resources.spriteSelCursor, dest, src, Color.White)

        let printFPS elapsed prevElapsed =
            let value =
                (1.0 / ((elapsed + prevElapsed) / 2.0))
                |> round
                |> string
            spriteBatch.DrawString (Resources.fontMain, value, (Vector2(4.0f, 4.0f)), Color.DarkRed)
        

        override this.Initialize () =
            do graphics.GraphicsDevice.SamplerStates.[0] <- SamplerState.PointClamp
            // Set screen size
            do graphics.PreferredBackBufferHeight <- Config.internalHeight
            do graphics.PreferredBackBufferWidth <- Config.internalWidth
            do graphics.ApplyChanges ()
            // Config
            do this.IsMouseVisible <- true
            do spriteBatch <- new SpriteBatch(this.GraphicsDevice)
            do base.Initialize ()
            ()

        override this.LoadContent() =
            Resources.load this.Content
            ()
 
        override this.Update (gameTime) =
            let delta = float32 gameTime.ElapsedGameTime.TotalSeconds
            do state <- 
                match state with
                | Loading ->
                    let controlState = { Camera = Camera.create (); HighlightedTile = None }
                    Playing (GameState.create (), Input.getInput (None), controlState)
                | Playing (gameState, inputState, controlState) ->
                    let input = Input.getInput (Some inputState)
                    let newControlState =
                        controlState
                        |> moveCamera input delta
                        |> findHighlightedTile gameState input
                    Playing (gameState, input, newControlState)
            ()
 
        override this.Draw (gameTime) =
            do this.GraphicsDevice.Clear Color.LightGray
            let elapsedSeconds = gameTime.ElapsedGameTime.TotalSeconds

            do spriteBatch.Begin ()

            // Draw tilemap
            match state with
            | Loading ->
                ()
            | Playing (gameState, _, controlState) ->
                do drawMap gameState.TileMap gameTime.TotalGameTime.TotalSeconds controlState.Camera.Pos
                if (controlState.HighlightedTile.IsSome)
                    then do drawHighlightedTile gameState.TileMap controlState.HighlightedTile.Value controlState.Camera
                    else ()
                ()

            // Draw frame rate
            do printFPS elapsedSeconds prevElapsedSeconds
        
            do spriteBatch.End ()
            do prevElapsedSeconds <- elapsedSeconds
            ()



