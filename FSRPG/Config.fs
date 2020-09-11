module Config

open System.IO
open System

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