namespace FSRPG.State

open FSRPG

// A team is just an int8, with the actual player being 0

type PathfindingInfo =
    {
        PossibleTiles: int list
    }

type WorldStateType =
    | Empty
    | Standby
    | SelectedActor of Game.Actor * PathfindingInfo

type WorldState =
    {
        Type: WorldStateType
        Players: List<int8>;
        TurnCount: int;
        PlayersTurn: int8;
        Actors: List<Game.Actor>;
        TileMap: MapHelpers.ActiveTilemap;
    }