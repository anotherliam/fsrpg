module Reducer

open FSRPG
open FSRPG.Actions
open FSRPG.State
open System.Collections.Generic

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
        // How long does pathfinding take????
        let timer = new System.Diagnostics.Stopwatch ()
        timer.Start ()
        let blockedIndexes = 
            gameState.Actors
            |> List.filter (fun other -> other.Team <> actor.Team)
            |> List.map (fun other -> gameState.TileMap.TilePointToTileIndex other.GridPosition)
            |> HashSet
        // Do all the pathfinding shit
        let possibleTiles =
            let nodes = PathFinding.findPaths blockedIndexes MovementType.Foot gameState.TileMap (gameState.TileMap.TilePointToTileIndex actor.GridPosition)
            nodes
            |> List.mapi (fun idx (cost, _) -> (cost <= 6.0f, idx)) // True/False for every square on the map if its accessable
            |> List.fold (fun result (valid, idx) ->
                match valid with
                | true -> idx::result
                | false -> result
            ) []
        let time = timer.ElapsedMilliseconds
        System.Diagnostics.Debug.WriteLine (sprintf "Pathfinding took: %ims" time)
        {
            gameState with
                Type = State.SelectedActor (actor, { PossibleTiles = possibleTiles } )
        }

    | UnselectActor ->
        {
            gameState with
                Type = State.Standby
        }

    // Moves an actor to a selected location and rests it
    // Todo: Should open menu to do attacks and shit
    | MoveActorTo (actor, loc) ->
        {
            gameState with
                Type = State.Standby;
                Actors = updateListItem gameState.Actors actor (fun a -> { a with GridPosition = loc; Tapped = true; });
        }