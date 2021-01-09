namespace FSRPG.GUIRendering


type Option = {
    name: string;
    id: int;
}

module UIMenu =
    open Microsoft.Xna.Framework
    open Microsoft.Xna.Framework.Graphics

    let marginHoriz = 4;
    let marginVert = 4;

    let getLongestOption options =
        let font = UIText.getUITextFont ()
        options
        |> List.map (fun opt -> (font.MeasureString opt.name).X)
        |> List.max
        |> int

    let drawMenu (sb: SpriteBatch) (options: Option List) (x: int) (y: int) (mouseX: int) (mouseY: int) =
        
        let textHeight = UIText.textHeight ()
        let totalHeight = (textHeight + marginVert) * options.Length + marginVert
        let totalWidth =
            options
            |> getLongestOption
            |> (fun width -> width + (marginHoriz * 2))
        let area = UIWindow.drawWindow sb x y totalWidth totalHeight
        // Draw text

        ()