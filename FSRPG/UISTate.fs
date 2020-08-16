namespace FSRPG

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

type UIState =
    {
        CameraZoom: float;
        CameraPosition: Vector2;
        Viewport: Viewport;
    }
    member this.Origin
        with get () =
            (new Vector2(((float32 this.Viewport.Width) / 2.0f), (float32 this.Viewport.Height) / 2.0f))

module UIState =
    
    ()
