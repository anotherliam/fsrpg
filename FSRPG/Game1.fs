namespace FSRPG

module GameRunner =

    open Microsoft.Xna.Framework
    open Microsoft.Xna.Framework.Graphics
    open Microsoft.Xna.Framework.Content
    open Microsoft.Xna.Framework.Input
    open MapHelpers
    open System
    open FSRPG.GUIRendering


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

        let textColor = Color.DarkSlateBlue

        

        let printFPS elapsed prevElapsed =
            let value =
                (1.0 / ((elapsed + prevElapsed) / 2.0))
                |> round
                |> string
            spriteBatch.DrawString (Resources.loaded.Fonts.Main_md, value, (Vector2(8.0f, 4.0f)), Color.DarkRed)
        
        let drawUIString font (str: string) x y =
            spriteBatch.DrawString (font, str, (Vector2(x, y)), Color.White)

        let drawUIText str x y = drawUIString Resources.loaded.Fonts.Main_md str x y
        let drawUITitle str x y = drawUIString Resources.loaded.Fonts.Main_sm str x y
        
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
                do printFPS delta prevElapsedSeconds
                // UI
                do match controlState.HighlightedTile with
                    | Some tile -> 
                        // Draw hovered unit window
                        match Game.WorldUtils.getActorOnTile worldState.Actors tile with
                        | Some highlightedUnit ->
                            do NineSlice.drawWindow spriteBatch (Resources.loaded.PrimaryWindowSkin) (new Rectangle (8, 24, 220, 120))
                            do drawUITitle highlightedUnit.Name 16.0f 32.0f
                            ()
                        | None -> ()
                        // Draw terrain window
                        match Game.WorldUtils.getTerrainForTile worldState.TileMap tile with
                        | terrain when terrain.InternalName = TileTerrainType.TerrainEmpty.InternalName -> ()
                        | terrain -> 
                            let top = Config.internalHeight - 100
                            do NineSlice.drawWindow spriteBatch (Resources.loaded.PrimaryWindowSkin) (new Rectangle (8, top, 180, 80))
                            do drawUITitle terrain.Name 16.0f (float32 top + 8.0f)
                            do drawUITitle (sprintf "%A" tile) 16.0f (float32 top + 24.0f)
                    | _ -> ()
                spriteBatch.End ()

                ()

            
        
            do prevElapsedSeconds <- delta
            ()



