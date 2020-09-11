namespace FSRPG.Game

open Microsoft.Xna.Framework
open System
open FSRPG

type ActorID = ActorID of Guid

type Actor =
    {
        ID: ActorID;
        Name: string;
        Class: string;
        Team: int8;
        GridPosition: Point;
        MovementType: MovementType;
    }
    static member blank () =
        {
            ID = Guid.NewGuid () |> ActorID;
            Name = "";
            Class = "";
            Team = 0y;
            GridPosition = new Point(0, 0);
            MovementType = MovementType.Foot;
        }