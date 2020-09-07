namespace FSRPG

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

type Camera =
    {
        X: float32;
        Y: float32;
        Zoom: float32;
        Viewport: Viewport;
    }

    // Creator boi
    static member create viewport = {
        X = 0.0f;
        Y = 0.0f;
        Zoom = 2.0f;
        Viewport = viewport;
    }

    // Getters
    member this.Pos with get () = new Vector2 (this.X, this.Y)

    member this.Origin
        with get () = (new Vector2(((float32 this.Viewport.Width) / 2.0f), (float32 this.Viewport.Height) / 2.0f))

    member this.ViewMatrix
        with get () =
            let origin = this.Origin
            (
                Matrix.CreateTranslation(new Vector3(-this.X, -this.Y, 0.0f)) *
                Matrix.CreateTranslation(new Vector3(-origin.X, -origin.Y, 0.0f)) *
                Matrix.CreateRotationZ(0.0f) *
                Matrix.CreateScale(this.Zoom, this.Zoom, 1.0f) *
                Matrix.CreateTranslation(new Vector3(origin.X, origin.Y, 0.0f))
            )

    // Camera transformations
    static member zoomFar cam = {
        cam with
            Zoom = 1.0f;
    }

    static member zoomNormal cam = {
        cam with
            Zoom = 2.0f;
    }

    static member centerOn (worldPos: Vector2) cam = {
        cam with
            X = worldPos.X - float32 (cam.Viewport.Width / 2);
            Y = worldPos.Y - float32 (cam.Viewport.Height / 2);
    }

    static member pan (by: Vector2) cam = {
        cam with
            X = cam.X + by.X;
            Y = cam.Y + by.Y;
    }

    // Utilities
    member this.ScreenToWorld (screenPos: Vector2) = Vector2.Transform(screenPos, Matrix.Invert (this.ViewMatrix))

    member this.WorldToScreen (worldPos: Vector2) = Vector2.Transform(worldPos, this.ViewMatrix)