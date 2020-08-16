namespace FSRPG

open Microsoft.Xna.Framework

type Camera =
    {
        X: float32;
        Y: float32;
        Zoom: float32;
    }
    // Creator boi
    static member create () = {
        X = 0.0f;
        Y = 0.0f;
        Zoom = 2.0f;
    }
    // Getters
    member this.Pos with get () = new Vector2 (this.X, this.Y)
    //
    member this.zoomClose () = {
        this with
            Zoom = 2.0f;
    }
    member this.zoomNormal () = {
        this with
            Zoom = 1.0f;
    }
    member this.pan (by: Vector2) = {
        this with
            X = this.X + by.X;
            Y = this.Y + by.Y;
    }