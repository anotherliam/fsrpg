module Config

open System.IO
open System
open Microsoft.Xna.Framework

let internalWidth = 960
let internalHeight = 540
let windowedWidth = 960
let windowedHeight = 540

let GridSize = 16

let MapSpeed = 300 // Scroll speed of map, pixels per second

let UnitMoveSpeed = 2 // Tiles per second

let IdleAnimFrameTime = 0.5

// Where we save games and shit
let SaveFolder = Path.Combine (Environment.CurrentDirectory, "saves");
let BattleSaveFile = Path.Combine (Environment.CurrentDirectory, "saves", "bsv.sav");

// Highlight colours
let MovementPossibiltyHLCol = Color (Color.Blue, 0.35f)
let ChosenEnemeyThreatHLCol = Color (Color.Red, 0.3f)

// Actor colours
let ActorColPlayer = Color (0, 215, 255)
let ActorColPlayerTapped = Color (96, 136, 136)
let ActorColEnemy = Color (255, 0, 0)
let ActorColEnemyTapped = Color (255, 33, 33)