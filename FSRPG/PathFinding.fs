namespace FSRPG

module PathFinding =
    open Microsoft.Xna.Framework
    open MapHelpers

    type Node =
        {
            Pos: Point;
            Index: int;
            Terrain: TileTerrainType;
            Neighbours: Node List Option;
            Excluded: bool;
            Cost: float;
        }

    let DIRS = [(1,0);(0,1);(-1,0);(0,-1)]

    let createNodeGraph (excludeIndexes: int list) (moveType: MovementType) (tmap: MapHelpers.ActiveTilemap) (mapCols, mapRows) =
        let tiles = tmap.Tiles
        let nodes =
            tiles
            |> List.mapi (fun idx tile ->
                let excluded = List.contains idx excludeIndexes
                let cost = if excluded then -1.0 else moveType |> tile.Terrain.MoveCost |> float
                {
                    Pos = new Point (tile.X, tile.Y);
                    Index = idx;
                    Terrain = tile.Terrain;
                    Excluded = excluded;
                    Cost = cost;
                    Neighbours = None
                }
            )
        let rec findNeighbours (node: Node) (dirs: (int * int) list) (neighbours: Node List): (Node List Option) =
            match dirs with
            | (x, y)::otherDirs ->
                let point = new Point(node.Pos.X + x, node.Pos.Y + y)
                if point.X >= 0 && point.X < mapCols && point.Y >= 0 && point.Y < mapRows
                    then findNeighbours node otherDirs (nodes.[tmap.TilePointToTileIndex point] :: neighbours)
                    else findNeighbours node otherDirs neighbours
            | _ -> if neighbours.Length >= 1 then Some neighbours else None
        nodes
        |> List.map (fun node ->
            {
                node with
                    Neighbours = findNeighbours node DIRS []
            }
        )
    
    let (|IsValidNextNode|_|) newCost candidate =
        match candidate with
        | None -> Some ()
        | Some currCost when newCost < currCost -> Some ()
        | _ -> None

    // todo: care about blocked indexes and movement types
    let findPaths blockedIndexes moveType (tmap: MapHelpers.ActiveTilemap) sourceIndex =
        let mapCols = tmap.Width
        let mapRows = tmap.Height

        // Returns a list of neighbours for the current tile
        let rec checkNeighbours (current: Point) (dirs: (int * int) list) (cameFrom: Map<int, int>) (costSoFar: Map<int, int>) (frontier: (Point * int) list) =
            match dirs with
            | (x, y)::tail ->
                let curIdx = tmap.TilePointToTileIndex current
                let nextPoint = Point(current.X + x, current.Y + y)
                let nextIndex = (tmap.TilePointToTileIndex nextPoint)
                if nextPoint.X >= 0 && nextPoint.X < mapCols && nextPoint.Y >= 0 && nextPoint.Y < mapRows
                    then
                        // Neighbour exists
                        let newCost = costSoFar.[tmap.TilePointToTileIndex current] + 1
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
        let rec next (nodes: Tile list) (cameFrom: Map<int, int>) (costSoFar: Map<int, int>) (frontier: (Point * int) list) =
            
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
        
        next tmap.Tiles (Map.ofList [(sourceIndex, -1)]) (Map.ofList [(sourceIndex, 0)]) [(tmap.TileIndexToTilePoint sourceIndex, 0)]

