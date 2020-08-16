namespace FSRPG

module Resources =

    open Microsoft.Xna.Framework.Graphics
    open Microsoft.Xna.Framework.Content
    open System.IO
    open JSONDefs
    open MapHelpers

    type TilesetTexture = Texture2D
    
    let TILESETS = [("OverworldTileset", "OverworldTileset.json")]
    let TILEMAPS = [("Test Map", "testmap.json"); ("Test Map 2", "testmap2.json")]

    // Fonts
    let mutable fontMain = Unchecked.defaultof<SpriteFont>
    let mutable fontBig = Unchecked.defaultof<SpriteFont>

    // Sprites
    let mutable spriteGrid = Unchecked.defaultof<Texture2D>
    let mutable spriteSelCursor = Unchecked.defaultof<Texture2D>

    // Maps
    let mutable tilesets: Map<string, (MapHelpers.Tileset * TilesetTexture)> = Map.empty
    let mutable tilemaps: Map<string, MapHelpers.Tilemap> = Map.empty

    let load (content: ContentManager) =
        do fontMain <- content.Load<SpriteFont> "fonts/main"
        do fontBig <- content.Load<SpriteFont> "fonts/big"
        do spriteGrid <- content.Load<Texture2D> "sprites/Grid"
        do spriteSelCursor <- content.Load<Texture2D> "sprites/SelectionCursor"
        do tilesets <-
            TILESETS
            |> List.map
                (fun (name, jsonName) ->
                    let parsed = MapHelpers.readTileset jsonName
                    let fileName = (sprintf "tiles/%s" (Path.GetFileNameWithoutExtension (parsed.ImageName)))
                    (name, (parsed, (content.Load<Texture2D> fileName)))
                )
            |> Map.ofList
        ()
        do tilemaps <-
            TILEMAPS
            |> List.map
                (fun (name, jsonName) ->
                    let parsed = MapHelpers.readTilemap jsonName
                    // Just see if we can find the tileset for this map, throwing an error if it doesnt exist
                    parsed.Tileset |> tilesets.TryFind |> function
                        | Some _ -> (name, parsed)
                        | None -> failwith (sprintf "Couldnt find tileset (%s) for %s" parsed.Tileset name)
                )
            |> Map.ofList

    let prepareActiveTilemap tilemapName: MapHelpers.ActiveTilemap =
        let tmap = tilemaps.Item tilemapName
        let tset, texture = tilesets.Item tmap.Tileset
        let getRegionRectForSpriteID id =
            let x = id % tset.Columns
            let y = id / tset.Columns
            (
                x * (tset.TileWidth + tset.TileSpacing * 2) + tset.TileSpacing,
                y * (tset.TileHeight + tset.TileSpacing * 2) + tset.TileSpacing
            )
        let tiles =
            seq<MapHelpers.Tile> {
                for x in 0 .. tmap.Width - 1 do
                    for y in 0 .. tmap.Height - 1 ->
                        let idx = y * tmap.Width + x
                        let layers =
                            tmap.Tiles
                            |> List.map (fun item -> item.[idx])
                            |> List.filter (fun item -> item <> 0)
                            |> List.map (fun item -> item - 1)
                            |> List.map (fun item ->
                                tset.Tiles |> List.tryFind (fun tile -> tile.ID = item)
                                |> function
                                    | Some tileType when (tileType.Animation.IsSome) ->
                                        // Animated tile
                                        tileType.Animation.Value
                                        |> List.map (fun animFrame -> getRegionRectForSpriteID animFrame.SpriteIndex)
                                        |> Animated
                                    | _ ->
                                        // Just a static tile
                                        Static (getRegionRectForSpriteID item)
                            )
                        {
                            Layers = layers;
                            X = x;
                            Y = y;
                        }
            }
            |> Seq.toList
            
            //tmap.Tiles
            //|> List.map (
            //    List.map (fun tile ->
            //        try
            //            tset.Tiles |> List.find (fun item -> item.ID = tile - 1)
            //        with
            //            | _ -> failwith (sprintf "Couldnt find tile %i" tile)
            //    )
            //)
        {
            Base = tmap;
            Tileset = tset;
            Tiles = tiles;
            Texture = texture;
        }

    let unload (content: ContentManager) =
        content.Unload ()