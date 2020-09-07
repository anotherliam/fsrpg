namespace FSRPG

module Resources =

    open Microsoft.Xna.Framework.Graphics
    open Microsoft.Xna.Framework.Content
    open System.IO
    open JSONDefs
    open MapHelpers
    open Microsoft.Xna.Framework
    open MonoGame.Extended.TextureAtlases
    open MonoGame.Extended

    let Thiccness = Thickness

    type TilesetTexture = Texture2D
    
    let TILESETS = [("Overworld", "Overworld.json")]
    let TILEMAPS = [("Ch0", "Ch0.json");]

    type FontResources =
        {
            Main_sm: SpriteFont;
            Main_md: SpriteFont;
        }
    
    type SpriteResources =
        {
            Grid: Sprite;
            SelCursor: Sprite;
            ActorSwordsman: Sprite;
        }

    type Resources =
        {
            Fonts: FontResources;
            Sprites: SpriteResources;
            TileSets: Map<string, (MapHelpers.Tileset * TilesetTexture)>;
            TileMaps: Map<string, MapHelpers.Tilemap>;
            PrimaryWindowSkin: NinePatchRegion2D;
        }

    
    let mutable loaded: Resources = Unchecked.defaultof<Resources>;
        

    let load (content: ContentManager) =
        
        let tilesets =
            TILESETS
            |> List.map
                (fun (name, jsonName) ->
                    let parsed = MapHelpers.readTileset jsonName
                    let fileName = (sprintf "tiles/%s" (Path.GetFileNameWithoutExtension (parsed.ImageName)))
                    (name, (parsed, (content.Load<Texture2D> fileName)))
                )
            |> Map.ofList

        let tilemaps =
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

        let windowSkinTexture = content.Load<Texture2D> ("ui/Window")

        loaded <- {
            Fonts =
                {
                    Main_sm = content.Load<SpriteFont> "fonts/Main_sm";
                    Main_md = content.Load<SpriteFont> "fonts/Main_md";
                }
            
            Sprites =
                {
                    Grid = Sprite.create
                        (content.Load<Texture2D> "sprites/Grid")
                        [new Rectangle (0,0,0,0)]
                        0.5;
                    SelCursor = Sprite.create
                        (content.Load<Texture2D> "sprites/SelectionCursor")
                        [new Rectangle (0,0,0,0)]
                        0.5
                    ActorSwordsman = ActorSprite.create
                        (content.Load<Texture2D> "sprites/Actors/Actor_Swordsman");
                }

            TileSets = tilesets;

            TileMaps = tilemaps;

            PrimaryWindowSkin = new NinePatchRegion2D (windowSkinTexture, Thiccness 6)
        }

    let prepareActiveTilemap resources tilemapName: MapHelpers.ActiveTilemap =
        let tmap = resources.TileMaps.Item tilemapName
        let tset, texture = resources.TileSets.Item tmap.Tileset
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