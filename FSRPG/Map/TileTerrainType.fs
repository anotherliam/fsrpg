namespace FSRPG

type TileTerrainType = {
    Name: string;
    MoveCost: int;
    Avoid: int;
    PhysDefence: int;
    MagDefence: int;
    PercHealthRec: int;
}


module TileTerrainType =
    let TerrainPlains = {
        Name = "Plains";
        MoveCost = 1;
        Avoid = 0;
        PhysDefence = 0;
        MagDefence = 0;
        PercHealthRec = 0;
    }