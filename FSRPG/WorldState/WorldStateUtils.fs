namespace FSRPG.State

// A team is just an int8, with the actual player being 0

module WorldStateUtils =
    open Microsoft.Xna.Framework
    open FSRPG

    let generateActors (): Game.Actor list =
        [
            {
                Game.Actor.blank () with
                    Name = "Robert";
                    GridPosition = new Point(1, 1);
            };
            {
                Game.Actor.blank () with
                    Name = "Johnny";
                    GridPosition = new Point(2, 2);
            };
            {
                Game.Actor.blank () with
                    Name = "Jim";
                    GridPosition = new Point(1, 2);
            };
            {
                Game.Actor.blank () with
                    Team = 1y;
                    Name = "Generic Enemy";
                    GridPosition = new Point(6, 3);
            };
            {
                Game.Actor.blank () with
                    Team = 1y;
                    Name = "Generic Enemy";
                    GridPosition = new Point(5, 4);
            };
            {
                Game.Actor.blank () with
                    Team = 1y;
                    Name = "Generic Enemy";
                    GridPosition = new Point(9, 1);
            };
            {
                Game.Actor.blank () with
                    Team = 1y;
                    Name = "Generic Enemy";
                    GridPosition = new Point(8, 2);
            };
            {
                Game.Actor.blank () with
                    Team = 1y;
                    Name = "Generic Enemy";
                    GridPosition = new Point(13, 2);
            };
            {
                Game.Actor.blank () with
                    Team = 1y;
                    Name = "Generic Enemy";
                    GridPosition = new Point(12, 3);
            };
            {
                Game.Actor.blank () with
                    Team = 1y;
                    Name = "Generic Enemy";
                    GridPosition = new Point(12, 5);
            };
            {
                Game.Actor.blank () with
                    Team = 1y;
                    Name = "Generic Enemy";
                    GridPosition = new Point(10, 5);
            };
            {
                Game.Actor.blank () with
                    Team = 1y;
                    Name = "Generic Enemy";
                    GridPosition = new Point(7, 7);
            };
            {
                Game.Actor.blank () with
                    Team = 1y;
                    Name = "Generic Enemy";
                    GridPosition = new Point(8, 8);
            };
            {
                Game.Actor.blank () with
                    Team = 1y;
                    Name = "Generic Enemy";
                    GridPosition = new Point(10, 8);
            };
        ]
    let createGenericGameState () =
        {
            Type = Standby;
            Players = [0y; 1y];
            TurnCount = 1;
            PlayersTurn = 0y;
            Actors = generateActors ();
            TileMap = Resources.prepareActiveTilemap Resources.loaded "Ch0";
        }