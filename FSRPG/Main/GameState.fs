namespace FSRPG.Main

open Microsoft.Xna.Framework

// Control state contains camera positions and highlighted tile information
// This is seperate from gamestate as it doesnt trigger transitions and isnt saved
type ControlState = {
    Camera: FSRPG.Camera;
    HighlightedTile: Option<Point>;
}

// The GameState object contains -everything- needed to run the game
// This includes UIState, Input details, and the WorldState

type GameStateObject = {
    WorldStateManager: StateManager.StateManager 
    InputState: FSRPG.Input.InputState
    ControlState: ControlState
}

type GameState =
    | Loading
    | Playing of GameStateObject

module GameState =
    ()
