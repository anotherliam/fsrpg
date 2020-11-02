namespace GameEngine

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

type AnimationStatus = Ongoing | Finished

type StaticSpriteSource =
    {
        Texture: Texture2D;
        Source: Rectangle;
    }


type AnimatedSpriteSource =
    {
        Texture: Texture2D;
        FrameTime: float; // Time in seconds that each frame lasts
        Frames: List<Rectangle>; // Source rectangles of each frame
        FrameCount: int; // Number of frames
        AnimLength: float; // Total length of the animation (FrameTime * FrameCount)
    }
    static member create texture frames frameTime =
        {
            Texture = texture;
            Frames = frames;
            FrameTime = frameTime;
            FrameCount = frames.Length;
            AnimLength = (float frames.Length) * frameTime;
        }

module StaticSpriteSource =
    open System

    let drawColored (sb: SpriteBatch) (spriteSrc: StaticSpriteSource) (pos: Vector2) color =
        sb.Draw (spriteSrc.Texture, pos, (Nullable spriteSrc.Source), color)

    let draw sb sprite pos = drawColored sb sprite pos Color.White
        
module AnimatedSpriteSource =
    open System

    let getFrame time frameTime animLength = int (floor ((time % animLength) / frameTime))

    let getFrameRepeating time sprite =
        getFrame time sprite.FrameTime sprite.AnimLength
        |> fun frame -> sprite.Frames.Item frame

    let getFrameOnce time sprite =
        if time > sprite.AnimLength
            then
                ((List.last sprite.Frames), Finished)
            else
                getFrame time sprite.FrameTime sprite.AnimLength
                |> fun frame -> ((sprite.Frames.Item frame), Ongoing)
    
    let drawColored (sb: SpriteBatch) time sprite (pos: Rectangle) color =
        let frame = Nullable (getFrameRepeating time sprite)
        sb.Draw (sprite.Texture, pos, frame, color)

    let draw sb time sprite pos = drawColored sb time sprite pos Color.White
 