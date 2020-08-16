namespace FSRPG

// A team is just an int8, with the actual player being 0


type GameStateType =
    | Empty
    | Standby

type GameState =
    {
        Type: GameStateType
        Players: List<int8>;
        TurnCount: int;
        PlayersTurn: int8;
        Units: List<Actor>;
        TileMap: MapHelpers.ActiveTilemap;
    }
    static member create () =
        {
            Type = Empty;
            Players = [0y; 1y];
            TurnCount = 1;
            PlayersTurn = 0y;
            Units = [];
            TileMap = Resources.prepareActiveTilemap "Test Map 2";
        }