module Reducer

open FSRPG
open FSRPG.Actions
open FSRPG.State

let updateListItem list item update =
    let rec iter oldList newList =
        match oldList with
        | head::tail when head = item -> iter tail ((update item)::newList)
        | head::tail -> iter tail (head::newList)
        | _ -> newList
    iter list []
    

let reduce (gameState: State.WorldState) (action: Actions.GameAction) =
    match action with

    | NoOp -> gameState

    | SelectActor actor ->
        {
            gameState with
                Type = State.SelectedActor (actor)
        }

    | UnselectActor ->
        {
            gameState with
                Type = State.Standby
        }

    | MoveActorTo (actor, loc) ->
        {
            gameState with
                Type = State.Standby;
                Actors = updateListItem gameState.Actors actor (fun a -> { a with GridPosition = loc; });
        }