namespace FSRPG

module GameRunner =

    open Microsoft.Xna.Framework
    open Microsoft.Xna.Framework.Graphics
    open Microsoft.Xna.Framework.Content
    open Microsoft.Xna.Framework.Input
    open MapHelpers
    open System
    open FSRPG.GUIRendering
    open FSRPG.GUIRendering.Windows


    type TransitionType<'t> =
        | Next of 't
        | Prev of int
        | Stay
    
    

    type Game1 () as this =
        inherit Game()
 
        do this.Content.RootDirectory <- "Content"
        do testing.runEvent testing.someEvent {values = 5}
        do testing.runWordsTest ()
        let graphics = new GraphicsDeviceManager(this)
        let mutable spriteBatch = Unchecked.defaultof<SpriteBatch>

        let mutable gameState: FSRPG.Main.GameState = FSRPG.Main.Loading

        // For fps
        let mutable prevElapsedSeconds = 0.0
        

        let printFPS elapsed prevElapsed =
            let value =
                (1.0 / ((elapsed + prevElapsed) / 2.0))
                |> round
                |> string
            spriteBatch.DrawString (Resources.loaded.Fonts.Main_md, value, (Vector2(8.0f, 4.0f)), Color.DarkRed)
        
        
        override this.Initialize () =
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
            do gameState <- FSRPG.Main.Update.tick graphics gameTime gameState
            ()
 
        override this.Draw (gameTime) =
            do this.GraphicsDevice.Clear Color.LightGray
            let delta = gameTime.ElapsedGameTime.TotalSeconds
            let time = gameTime.TotalGameTime.TotalSeconds

            // Draw tilemap
            match gameState with
            | FSRPG.Main.Loading ->
                ()
            | FSRPG.Main.Playing { WorldStateManager = worldStateManager; InputState = inputState; ControlState = controlState; } ->
                let worldState = worldStateManager.Current
                // Draw in world space
                spriteBatch.Begin (SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, Nullable controlState.Camera.ViewMatrix)
                // Draw map
                do FSRPG.Render.DrawUtils.drawMap spriteBatch worldState.TileMap time
                // Draw units
                do worldState.Actors
                |> List.iter (fun actor ->
                    let isSelected =
                        match worldState.Type with
                        | State.SelectedActor selected when selected = actor -> true
                        | _ -> false
                    do FSRPG.Render.DrawUtils.drawMapActor spriteBatch actor isSelected time
                )
                // Draw selector
                if (controlState.HighlightedTile.IsSome)
                    then do FSRPG.Render.DrawUtils.drawHighlightedTile spriteBatch controlState.HighlightedTile.Value time
                    else ()
                spriteBatch.End ()

                // Draw in screen space
                spriteBatch.Begin ()
                // frame rate
                // do printFPS delta prevElapsedSeconds
                // UI
                do match controlState.HighlightedTile with
                    | Some tile -> 
                        // Draw hovered unit window
                        match Game.WorldUtils.getActorOnTile worldState.Actors tile with
                        | Some highlightedUnit ->
                            do HoveredUnitWindow.draw spriteBatch highlightedUnit inputState.MousePosition
                            ()
                        | None -> ()
                        // Draw terrain window
                        match Game.WorldUtils.getTerrainForTile worldState.TileMap tile with
                        | terrain when terrain.InternalName = TileTerrainType.TerrainEmpty.InternalName -> ()
                        | terrain -> do TerrainWindow.draw spriteBatch terrain tile inputState.MousePosition
                    | _ -> ()
                spriteBatch.End ()

                ()

            
        
            do prevElapsedSeconds <- delta
            ()



