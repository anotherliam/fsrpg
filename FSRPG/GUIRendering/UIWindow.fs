namespace FSRPG.GUIRendering

module UIWindow =

    open FSRPG
    open Microsoft.Xna.Framework

    let PADDING = 2.0f;

    let drawWindowFromRect sb rect =
        NineSlice.drawWindow sb (Resources.loaded.PrimaryWindowSkin) rect

    let drawWindow sb x y width height =
        drawWindowFromRect sb (new Rectangle (x, y, width, height))
        new Vector2(float32 x + PADDING, float32 y + PADDING)

