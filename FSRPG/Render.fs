namespace FSRPG.Render

module DrawUtils =
    open Microsoft.Xna.Framework
    open Microsoft.Xna.Framework.Graphics
    open FSRPG
    open System

    let drawMap (spriteBatch: SpriteBatch) (tmap: FSRPG.MapHelpers.ActiveTilemap) (time: float) =
        let getFrame (anim: List<int * int>) =
            let t = time % ((float anim.Length) * 0.5)
            anim.[int (floor (t / 0.5))]
        let w = tmap.Tileset.TileWidth
        let h = tmap.Tileset.TileHeight
        let _, texture = Resources.loaded.TileSets.Item tmap.TilesetName
        let rec drawTile: (List<FSRPG.MapHelpers.Tile> -> unit) = function
            | tile::tail ->
                let destX = float (tile.X * w)
                let destY = float (tile.Y * h)
                let dest = new Vector2 ((floor destX) |> float32, (floor destY) |> float32)
                tile.Layers
                |> List.iter ( fun tile ->
                    match tile with
                    | FSRPG.MapHelpers.Animated anim ->
                        let (x, y) = getFrame anim
                        System.Nullable<Rectangle>(new Rectangle(x, y, w, h))
                    | FSRPG.MapHelpers.Static (x, y) ->
                        System.Nullable<Rectangle>(new Rectangle(x, y, w, h))
                    |> fun src -> do spriteBatch.Draw (texture, dest, src, Color.White)   
                )
                drawTile tail
            | [] -> ()
        drawTile tmap.Tiles
        ()

    let drawHighlightedTile (spriteBatch: SpriteBatch) (tile: Point) (time: float) =
        let size = int ((float Config.GridSize) * 1.5)
        let offset = int ((float Config.GridSize) * 0.25)
        let dest = Rectangle((tile.X * Config.GridSize) - offset, (tile.Y * Config.GridSize) - offset, size, size)
                
        let src =
            if floor (time % 2.0) = 0.0
                then Nullable (new Rectangle (0, 0, 32, 32))
                else Nullable (new Rectangle (0, 32, 32, 32))
        do spriteBatch.Draw (Resources.loaded.Sprites.SelCursor.Texture, dest, src, Color.White)
        ()
        
    let drawRec (sb: SpriteBatch) (pos: Rectangle) col =
        do sb.Draw (Resources.loaded.BlankTexture, pos, col)
        ()

    let getActorCol team selected tapped time =
        let baseCol = 
            match team with
            | 0y when not tapped -> Config.ActorColPlayer
            | 0y -> Config.ActorColPlayerTapped
            | _ when not tapped -> Config.ActorColEnemy
            | _ -> Config.ActorColEnemyTapped
        let alpha =
            if selected then
                if ((time % 1.0) <= 0.5) then 0.2f
                else 0.6f
            else 1.0f
        new Color(baseCol, alpha)

    let drawMapActor (spriteBatch: SpriteBatch) (actor: Game.Actor) (selected: bool) (time: float) =
        let dest = new Rectangle((actor.GridPosition.X * Config.GridSize), (actor.GridPosition.Y * Config.GridSize), Config.GridSize, Config.GridSize)
        let spr = Resources.loaded.Sprites.ActorSwordsman
        let src = Sprite.getFrameRepeating time spr |> Nullable
        let col = getActorCol actor.Team selected actor.Tapped time
        do spriteBatch.Draw (spr.Texture, dest, src, col)
        ()
