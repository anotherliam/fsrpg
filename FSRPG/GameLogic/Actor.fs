namespace FSRPG.Game

open Microsoft.Xna.Framework
open System
open FSRPG

type ActorID = ActorID of Guid

type Statistics = {
    MHP: int;
    Atk: int;
    Def: int;
    Res: int;
    Agi: int;
    Dex: int;
    Luk: int;
}

type DerivedStatistics = {
    Pow: int;
    PProt: int;
    MProt: int;
    Hit: int;
    Dodge: int;
    Crit: int;
    CritDodge: int;
}

module Statistics =
    let getBlankStatistics () =
        {
            MHP = 0;
            Atk = 0;
            Def = 0;
            Res = 0;
            Agi = 0;
            Dex = 0;
            Luk = 0;
        }

    let getGenericStatistics () =
        {
            MHP = 25;
            Atk = 8;
            Def = 8;
            Res = 8;
            Agi = 8;
            Dex = 8;
            Luk = 8;
        }

    
    

type Actor =
    {
        ID: ActorID;
        Name: string;
        Class: string;
        Team: int8;
        GridPosition: Point;
        MovementType: MovementType;
        Level: int;
        CHP: int;
        Statistics: Statistics;
        Weapon: Weapon Option
        Tapped: Boolean;
    }
    static member blank () =
        {
            ID = Guid.NewGuid () |> ActorID;
            Name = "";
            Class = "";
            Team = 0y;
            GridPosition = new Point(0, 0);
            MovementType = MovementType.Foot;
            Level = 1;
            CHP = 20;
            Statistics = Statistics.getGenericStatistics ();
            Weapon =
                WeaponPrototypeID "longsword"
                |> Weapon.createInstance
                |> Some
            Tapped = false;
        }
    member this.calcDerivedStatistics () =
        match this.Weapon with
        | Some weapon ->
            let weaponStats = weapon.Prototype ()
            {
                Pow = this.Statistics.Atk + weaponStats.Pow;
                PProt = this.Statistics.Def;
                MProt = this.Statistics.Res;
                Hit = (this.Statistics.Dex * 2) + weaponStats.Acc;
                Dodge = this.Statistics.Agi + this.Statistics.Luk;
                Crit = this.Statistics.Dex;
                CritDodge = this.Statistics.Luk;
            }
        | None ->
            {
                Pow = 0;
                PProt = this.Statistics.Def;
                MProt = this.Statistics.Res;
                Hit = 0;
                Dodge = this.Statistics.Agi + this.Statistics.Luk;
                Crit = 0;
                CritDodge = this.Statistics.Luk;
            }
    