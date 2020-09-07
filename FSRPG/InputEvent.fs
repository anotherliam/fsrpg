module InputEvent

open FSRPG
open Microsoft.Xna.Framework

// This module handles dealing with (mouse) inputs from the player


type MouseOn =
    | Bubble // Bubble means the event should continue through other ui functions
    | Empty
    | MapTile of Point


let handleMapInput (map: MapHelpers.ActiveTilemap) (inputState: Input.InputState) (mouseWorldPos: Vector2) =
    // Only if the mouse position is within the map
    let mapSize = new Rectangle (0, 0, map.PixelWidth, map.PixelHeight)
    if mapSize.Contains (mouseWorldPos)
        then 
            let x = floor ((float mouseWorldPos.X) / (float map.Tileset.TileWidth))
            let y = floor ((float mouseWorldPos.Y ) / (float map.Tileset.TileHeight))
            MapTile (new Point (int x, int y))
        else Bubble
