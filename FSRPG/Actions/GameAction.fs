namespace FSRPG.Actions

open FSRPG
open Microsoft.Xna.Framework

// A GameAction describs the change from one game state to another

type GameAction =
    | NoOp
    | SelectActor of Game.Actor
    | UnselectActor
    | MoveActorTo of Game.Actor * Point