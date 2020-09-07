namespace FSRPG

module ActorSprite =
    open Microsoft.Xna.Framework

    let baseFrames = [(new Rectangle(0, 0, 16, 16)); (new Rectangle(16, 0, 16, 16))];

    let create texture: Sprite =
        {
            Texture = texture;
            Frames = baseFrames;
            FrameTime = Config.IdleAnimFrameTime;
            FrameCount = baseFrames.Length;
            AnimLength = Config.IdleAnimFrameTime * (float baseFrames.Length);
        }