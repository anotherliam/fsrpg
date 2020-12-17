namespace FSRPG

type MovementCost = MovementType -> float32

type TileTerrainType = {
    InternalName: string;
    Name: string;
    MoveCost: MovementCost;
    Avoid: int;
    PhysDefence: int;
    MagDefence: int;
    PercHealthRec: int;
}


module TileTerrainType =

    // Move cost sets
    let basicMovementCosts _ = 1.0f
    let impassableMovementCost _ = 999.0f
    let forestMovementCost: MovementCost = function
        | Foot -> 2.0f
        | Flying -> 1.0f
        | Mounted -> 3.0f
    let roadMovementCost: MovementCost = function
        | Foot -> 0.7f
        | Flying -> 1.0f
        | Mounted -> 0.7f
    
    // Terrains
    
    let TerrainPlains = {
        InternalName = "plains"
        Name = "Plains";
        MoveCost = basicMovementCosts;
        Avoid = 0;
        PhysDefence = 0;
        MagDefence = 0;
        PercHealthRec = 0;
    }
    let TerrainForest = {
        InternalName = "forest"
        Name = "Forest";
        MoveCost = forestMovementCost;
        Avoid = 25;
        PhysDefence = 1;
        MagDefence = 0;
        PercHealthRec = 0;
    }
    let TerrainThicket = {
        InternalName = "thicket"
        Name = "Thicket";
        MoveCost = forestMovementCost;
        Avoid = 40;
        PhysDefence = 1;
        MagDefence = 0;
        PercHealthRec = 0;
    }
    let TerrainFort = {
        InternalName = "fort"
        Name = "Fort";
        MoveCost = basicMovementCosts;
        Avoid = 25;
        PhysDefence = 2;
        MagDefence = 2;
        PercHealthRec = 10;
    }
    let TerrainRoad = {
        InternalName = "road"
        Name = "Road";
        MoveCost = roadMovementCost;
        Avoid = -10;
        PhysDefence = 0;
        MagDefence = 0;
        PercHealthRec = 0;
    }
    let TerrainBridge = {
        InternalName = "bridge"
        Name = "Bridge";
        MoveCost = basicMovementCosts;
        Avoid = 0;
        PhysDefence = 0;
        MagDefence = 0;
        PercHealthRec = 0;
    }
    let TerrainWater = {
        InternalName = "water"
        Name = "Water";
        MoveCost = impassableMovementCost;
        Avoid = 0;
        PhysDefence = 0;
        MagDefence = 0;
        PercHealthRec = 0;
    }
    let TerrainEmpty = {
        InternalName = "empty"
        Name = "";
        MoveCost = impassableMovementCost;
        Avoid = 0;
        PhysDefence = 0;
        MagDefence = 0;
        PercHealthRec = 0;
    }
    let TerrainCastle = {
        InternalName = "castle_back"
        Name = "Castle";
        MoveCost = impassableMovementCost;
        Avoid = 0;
        PhysDefence = 0;
        MagDefence = 0;
        PercHealthRec = 0;
    }
    let TerrainCastleGate = {
        InternalName = "castle"
        Name = "Castle Gate";
        MoveCost = basicMovementCosts;
        Avoid = 30;
        PhysDefence = 2;
        MagDefence = 2;
        PercHealthRec = 20;
    }

    let getTerrainType = function
        | "plains" -> TerrainPlains
        | "forest" -> TerrainForest
        | "thicket" -> TerrainThicket
        | "fort" -> TerrainFort
        | "road" -> TerrainRoad
        | "bridge" -> TerrainBridge
        | "water" -> TerrainWater
        | "castle_corner"
        | "castle_back" -> TerrainCastle
        | "castle" -> TerrainCastleGate
        | ""
        | "empty"
            -> TerrainEmpty
        | terrain ->
            sprintf "%s not found" terrain
            |> failwith