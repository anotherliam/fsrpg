namespace FSRPG.Game

open System

type WeaponPrototypeID = WeaponPrototypeID of string

type WeaponPrototype =
    {
        ID: WeaponPrototypeID;
        DisplayName: string;
        Pow: int;
        Acc: int;
        Dur: int;
        Cost: int;
    }
    member this.TotalCost () = this.Cost * this.Dur;

// Contains all the loaded weapons for the game
module Weapons =
    let weapons =
        [
            (
                WeaponPrototypeID "longsword",
                {
                    ID = WeaponPrototypeID "longsword";
                    DisplayName = "Longsword";
                    Pow = 6;
                    Acc = 90;
                    Dur = 25;
                    Cost = 30;
                };
            )
        ]
        |> Map.ofList
    let getWeaponById id =
        match weapons.TryFind id with
        | Some weapon -> weapon
        | None -> failwith (sprintf "weapon %A does not exist" id)


type Weapon =
    {
        PrototypeID: WeaponPrototypeID;
        RemainingDurability: int;
        DisplayName: string;
    }
    static member createInstance id =
        let prototype = Weapons.getWeaponById id
        {
            PrototypeID = id;
            RemainingDurability = prototype.Dur;
            DisplayName = prototype.DisplayName;
        }
    member this.Prototype () = Weapons.getWeaponById this.PrototypeID
    member this.Worth () = (this.Prototype ()).Cost * this.RemainingDurability


module Weapon =
    ()