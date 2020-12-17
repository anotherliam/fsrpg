namespace FSRPG

module PathFinding =
    open Microsoft.Xna.Framework
    open MapHelpers
    open System
    open System.Collections.Generic
    
    let DIRS = [(1,0);(0,1);(-1,0);(0,-1)]
    
    let (|IsValidNextNode|_|) newCost candidate =
        match candidate with
        | None -> Some ()
        | Some currCost when newCost < currCost -> Some ()
        | _ -> None
        
    // visited Hashset of already visited node indexes
    // Map of node idx -> node idx that we came from
    // CostSoFar - Map of node idx -> cost so far for that idx
    // Frontier - Tiles to look at from current tile, tuple of actual tile * cost

    let findPaths (blockedIndexes: int HashSet) (moveType: MovementType) (tmap: MapHelpers.ActiveTilemap) (sourceIndex: int) =
        let mapCols = tmap.Width
        let mapRows = tmap.Height

        // Returns true if theres nothing blocking the given tile
        let inline isAccessable idx = idx |> blockedIndexes.Contains |> not

        // Find all neighbours that have not yet been visited
        let rec findNeighbours (dirs: (int * int) list) currentNode (visited: int HashSet) (neighbours: (Tile * int) list) =
            match dirs with
            | (x, y)::tail ->
                let neighbourPoint = Point (currentNode.X + x, currentNode.Y + y)
                let neighbourIdx = tmap.TilePointToTileIndex neighbourPoint
                if (neighbourPoint.X >= 0 && neighbourPoint.X < mapCols && neighbourPoint.Y >= 0 && neighbourPoint.Y < mapRows)
                    && (not (visited.Contains neighbourIdx))
                    && (isAccessable neighbourIdx)
                then findNeighbours tail currentNode visited ((tmap.Tiles.[neighbourIdx], neighbourIdx)::neighbours)
                else findNeighbours tail currentNode visited neighbours
            | _ -> neighbours

        let rec processNeighbours currentNodeIndex (visited: int HashSet) (cameFrom: Map<int, int>) frontier (costSoFar: Map<int, float32>) (neighbours: (Tile * int) list) =
            match neighbours with
            | (neighbour, neighbourIdx)::tail ->
                let moveCost = neighbour.Terrain.MoveCost moveType
                let newCost = costSoFar.[currentNodeIndex] + moveCost
                do visited.Add neighbourIdx |> ignore // yuck - maybe use something else
                let newCameFrom = cameFrom |> Map.add neighbourIdx currentNodeIndex
                let newFrontier = (neighbour, newCost)::frontier
                let newCostSoFar = costSoFar |> Map.add neighbourIdx newCost
                processNeighbours currentNodeIndex visited newCameFrom newFrontier newCostSoFar tail
            | [] -> (visited, cameFrom, frontier, costSoFar)
                        

        let rec tick visited (cameFrom: Map<int, int>) costSoFar (frontier: (Tile * float32) list) =
            frontier
            |> List.sortBy (fun (_, cost) -> cost)
            |> function
            | (currentNode, currentNodeCost)::frontierTail ->
                let currentNodeIndex = tmap.TilePointToTileIndex (Point (currentNode.X, currentNode.Y))
                let (newVisited, newCameFrom, newFrontier, newCostSoFar) =
                    findNeighbours DIRS currentNode visited []
                    |> processNeighbours currentNodeIndex visited cameFrom frontierTail costSoFar
                tick newVisited newCameFrom newCostSoFar newFrontier
            | [] ->
                seq {
                    for idx in 0..(tmap.Tiles.Length) ->
                        costSoFar.TryFind idx
                        |> function
                        | Some cost -> (cost, cameFrom.[idx])
                        | None ->  (9999.0f, 0) // ?wtf is this
                }
                |> Seq.toList

        tick (HashSet [sourceIndex]) (Map.ofList [(sourceIndex, -1)]) (Map.ofList [(sourceIndex, 0.0f)]) [(tmap.Tiles.[sourceIndex], 0.0f)]

    // todo: just write better pathfinding code wtf is this shit it takes like 200ms?????
    // todo: care about movement types
    let findPathsSlow (blockedIndexes: int HashSet) moveType (tmap: MapHelpers.ActiveTilemap) sourceIndex =
        let mapCols = tmap.Width
        let mapRows = tmap.Height

        let moveCosts = tmap.Tiles |> List.mapi (fun idx tile -> (idx, tile.Terrain.MoveCost)) |> dict

        let inline isAccessable idx = blockedIndexes.Contains (idx) |> not

        // Returns a list of neighbours for the current tile
        let rec checkNeighbours (current: Point) (dirs: (int * int) list) (cameFrom: Map<int, int>) (costSoFar: Map<int, float32>) (frontier: (Point * float32) list) =
            match dirs with
            | (x, y)::tail ->
                let curIdx = tmap.TilePointToTileIndex current
                let nextPoint = Point(current.X + x, current.Y + y)
                let nextIndex = (tmap.TilePointToTileIndex nextPoint)
                // Check if the neighbour exists
                if (nextPoint.X >= 0 && nextPoint.X < mapCols && nextPoint.Y >= 0 && nextPoint.Y < mapRows)
                    then
                        // Neighbour exists
                        let costToAdd =
                            if (isAccessable nextIndex)
                                then
                                    (moveCosts.[nextIndex]) moveType
                                else 9999.0f
                        let newCost = costSoFar.[tmap.TilePointToTileIndex current] + costToAdd
                        match Map.tryFind nextIndex costSoFar with
                        | IsValidNextNode newCost ->
                        checkNeighbours
                            current
                            tail
                            (Map.add nextIndex curIdx cameFrom)
                            (Map.add nextIndex newCost costSoFar)
                            ((nextPoint, newCost)::frontier)
                        | _ -> checkNeighbours current tail cameFrom costSoFar frontier
                    else
                        // Neighbour doesnt exist
                        checkNeighbours current tail cameFrom costSoFar frontier
            | _ -> (cameFrom, costSoFar, frontier)
            
        // Iterator
        let rec next (nodes: Tile list) (cameFrom: Map<int, int>) (costSoFar: Map<int, float32>) (frontier: (Point * float32) list) =
            
            match frontier with
            | (current, _)::tail ->
                // Iterate through neighbours
                checkNeighbours current DIRS cameFrom costSoFar tail
                |> fun (cf, csf, f) -> next nodes cf csf f
            | [] ->
                costSoFar
                |> Map.toList
                |> List.map (fun (idx, cost) ->
                    (cost, cameFrom.[idx])
                )
        
        let result = next tmap.Tiles (Map.ofList [(sourceIndex, -1)]) (Map.ofList [(sourceIndex, 0.0f)]) [(tmap.TileIndexToTilePoint sourceIndex, 0.0f)]
        result

