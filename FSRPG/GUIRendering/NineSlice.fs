namespace FSRPG.GUIRendering

open Microsoft.Xna.Framework
open MonoGame.Extended.TextureAtlases

module NineSlice =
    open Microsoft.Xna.Framework.Graphics

    let drawWindow (spriteBatch: SpriteBatch) (ninePatch: NinePatchRegion2D) location =
        spriteBatch.Draw (ninePatch, location, Color.White)
    