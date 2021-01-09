namespace FSRPG.GUIRendering

module UIText =
    open FSRPG
    open Microsoft.Xna.Framework.Graphics
    open Microsoft.Xna.Framework
    
    let textColor = Color.White

    let textHeight () = Resources.loaded.Fonts.Main_sm.LineSpacing
    let titleHeight () = Resources.loaded.Fonts.Main_md.LineSpacing

    let getUITextFont () = Resources.loaded.Fonts.Main_sm
    let getUITitleFont () = Resources.loaded.Fonts.Main_md

    let drawUIString (sb: SpriteBatch) (font: SpriteFont) (str: string) x y =
        sb.DrawString (font, str, (Vector2(float32 x, float32 y)), textColor)

    let drawUIStringCenteredHorizontally (sb: SpriteBatch) (font: SpriteFont) (str: string) x y =
        let measure = font.MeasureString str
        let x = int (float32 x - (measure.X / 2.0f))
        drawUIString sb font str x y

    let drawUIStringAlignedRight (sb: SpriteBatch) (font: SpriteFont) (str: string) x y =
        let measure = font.MeasureString str
        let x = int (float32 x - measure.X)
        drawUIString sb font str x y

    let drawUIText sb str x y = drawUIString sb (getUITextFont ()) str x y
    let drawUITitle sb str x y = drawUIString sb (getUITitleFont ()) str x y

    let drawUITextCentered sb str x y = drawUIStringCenteredHorizontally sb (getUITextFont ()) str x y
    let drawUITitleCentered sb str x y = drawUIStringCenteredHorizontally sb (getUITitleFont ()) str x y

    let drawUITextRight sb str x y = drawUIStringAlignedRight sb (getUITextFont ()) str x y
    let drawUITitleRight sb str x y = drawUIStringAlignedRight sb (getUITitleFont ()) str x y