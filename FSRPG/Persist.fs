namespace FSRPG

open Newtonsoft.Json

module Persist =
    open System.IO

    let setupSaveFolder () =
        Directory.CreateDirectory Config.SaveFolder

    // Converts the worldState object into a json string
    let persistToJson (worldState: State.WorldState) =
        do System.Diagnostics.Debug.WriteLine "Converting WorldState to JSON"
        JsonConvert.SerializeObject worldState

    // Converts a json string into a worldState object
    let restoreFromJson (json: string) =
        do System.Diagnostics.Debug.WriteLine "Converting JSON to WorldState"
        JsonConvert.DeserializeObject<State.WorldState> json

    let loadGameFromFile fileName =
        do setupSaveFolder () |> ignore
        match File.Exists fileName with
        | true ->
            File.ReadAllText fileName
            |> restoreFromJson
        | false -> failwith "Couldnt load file - doesnt seem to exist"
        

    let saveGameToFile fileName worldState =
        do setupSaveFolder () |> ignore
        persistToJson worldState
        |> fun json -> File.WriteAllText (fileName, json)

    let battleSave = saveGameToFile Config.BattleSaveFile

    let battleLoad () = loadGameFromFile Config.BattleSaveFile