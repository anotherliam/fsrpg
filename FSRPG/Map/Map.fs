namespace FSRPG


module MapHelpers =


    open FSharp.Data
    open System.IO
    open Microsoft.Xna.Framework.Graphics

    // This module does map related shit idk man

    type TilesetJSONType = FSharp.Data.JsonProvider<JSONDefs.tileset>
    type TilemapJSONType = FSharp.Data.JsonProvider<JSONDefs.tilemap>

    type TileAnimationFrame = {
        SpriteIndex: int;
        Duration: int;
    }

    type TileType = {
        ID: int;
        Terrain: TileTerrainType
        SpriteIndex: int;
        Animation: Option<List<TileAnimationFrame>>
    }
    // Contains info about a specific tileset (loading from Tiled)
    type Tileset = {
        Columns: int;
        ImageName: string;
        // Height and Width of each tile, in pixels
        TileHeight: int;
        TileWidth: int;
        // Spacing between each tile in the tilesheet
        TileSpacing: int;
        // Actual tile definitions
        Tiles: List<TileType>
    }

    // A single layer within a tilemap
    type TileLayer = List<int>

    // A tilemap but its just ints really :ok_hand:
    type Tilemap = {
        Tileset: string;
        Height: int;
        Width: int;
        Tiles: List<TileLayer>;
    }

    // A sprite within an active tilemap
    type TileSprite =
        | Animated of List<int * int>
        | Static of int * int

    // Each tile contains its own layers
    type Tile = {
        Layers: List<TileSprite>; // Coords of the sprite in the texture
        // Actual location of the tile
        X: int;
        Y: int;
    }

    // A tilemap with all the references ready, ready for rendering
    type ActiveTilemap =
        {
            Base: Tilemap;
            Tileset: Tileset;
            Tiles: List<Tile>;
            Texture: Texture2D;
        }
        member this.Height with get () = this.Base.Height
        member this.Width with get () = this.Base.Width
        member this.PixelHeight with get () = this.Height * this.Tileset.TileHeight
        member this.PixelWidth with get () = this.Width * this.Tileset.TileWidth
        member this.TilesetName with get () = this.Base.Tileset

    let parseAnimation (animation: TilesetJSONType.Animation[]) =
        let parseFrame (frame: TilesetJSONType.Animation) = {
            SpriteIndex = frame.Tileid;
            Duration = frame.Duration;
        }
        if animation.Length >= 1
            then
                animation
                |> Seq.map parseFrame
                |> Seq.toList
                |> Some
            else None

    let readTileset fileName =
        let parsed =
            File.ReadAllText ("./Content/data/tilesets/" + fileName)
            |> TilesetJSONType.Parse
        let tiles =
            parsed.Tiles
            |> Seq.map (fun tile ->
                {
                    ID = tile.Id;
                    Terrain = TileTerrainType.TerrainPlains;
                    SpriteIndex = tile.Id;
                    Animation = parseAnimation tile.Animation
                }
            )
            |> Seq.toList
        {
            Columns = parsed.Columns;
            TileHeight = parsed.Tileheight;
            TileWidth = parsed.Tilewidth;
            TileSpacing = parsed.Spacing; // Theres also a parsed.margin, may need testing, but ill always just set margin to 0 so whatever i guess
            Tiles = tiles;
            ImageName = parsed.Image;
        }

    let readTilemap fileName =
        let parsed =
            File.ReadAllText ("./Content/data/maps/" + fileName)
            |> TilemapJSONType.Parse
        let layers =
            parsed.Layers
            |> Seq.map (fun layer ->
                layer.Data |> Seq.toList
            )
            |> Seq.toList
        {
            Tileset = (Path.GetFileNameWithoutExtension (parsed.Tilesets.[0].Source));
            Width = parsed.Width;
            Height = parsed.Height;
            Tiles = layers;
        }