namespace FSRPG

module PathFinding =
    open Microsoft.Xna.Framework

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
