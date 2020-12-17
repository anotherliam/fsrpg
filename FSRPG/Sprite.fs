namespace FSRPG

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

type AnimationStatus = Ongoing | Finished


type Sprite =
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
        
module Sprite =
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
    
    let dataset = [2;5;2]

    let sum dataset =
        dataset
        |> List.fold (fun acc item -> acc + item) 0


    let rec fold<'T, 'Acc> folder (acc: 'Acc) (list: 'T list) =
        match list with
        | [] -> acc
        | head::tail -> fold folder (folder acc head) tail