namespace FSRPG.Game

open Microsoft.Xna.Framework

type Actor =
    {
        Name: string;
        Team: int8;
        GridPosition: Point
    }
    static member blank () =
        {
            Name = "";
            Team = 0y;
            GridPosition = new Point(0, 0)
        }