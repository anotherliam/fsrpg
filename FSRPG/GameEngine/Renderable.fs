namespace GameEngine

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

// A Renderable is one of the outputs of a Node
// Renderables are generally just Sprites
// They take the current game time

type Renderable = SpriteBatch -> float32 -> unit

module Renderable =

    let Sprite pos src = ( fun sb time -> do StaticSpriteSource.draw sb src pos )
    
    let DebugText text = ( fun sb time -> do System.Diagnostics.Debug.WriteLine text )