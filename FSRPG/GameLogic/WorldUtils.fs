namespace FSRPG.Game

open Microsoft.Xna.Framework

module WorldUtils =
    open FSRPG.MapHelpers

    let getActorOnTile (actors: List<Actor>) (gridPos: Point) =
        actors |>
        List.tryFind (fun actor -> actor.GridPosition = gridPos)
    let getTerrainForTile (tmap: ActiveTilemap) gridPos =
        let idx =
            gridPos
            |> tmap.TilePointToTileIndex
        let tile = idx |> fun idx -> tmap.Tiles.[idx]
        tile.Terrain;