namespace FSRPG.Game

open Microsoft.Xna.Framework

module WorldUtils =
    let getActorOnTile (actors: List<Actor>) (gridPos: Point) =
        actors |>
        List.tryFind (fun actor -> actor.GridPosition = gridPos)