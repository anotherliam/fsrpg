namespace FSRPG.GUIRendering.Windows

module HoveredUnitWindow =
    open FSRPG.GUIRendering
    open FSRPG
    open Microsoft.Xna.Framework
    open MonoGame.Extended
    
    let width = 260
    let height = 120
    let normalPosition = Rectangle (8, 16, width, height)
    let offsetPosition = Rectangle (8 + width, 16, width, height)

    let draw sb (actor: Game.Actor) (mousePos: Vector2) =
        let pos = if normalPosition.Contains mousePos then offsetPosition else normalPosition
        let titleSpacing = UIText.titleHeight () + 4
        let textSpacing = UIText.textHeight () + 4
        let baseY = pos.Y + 8
        let baseX = pos.X + 8
        do UIWindow.drawWindowFromRect sb pos |> ignore
        // draw name
        do UIText.drawUITitleCentered sb actor.Name (pos.X + (pos.Width / 2)) baseY
        // draw hp
        do UIText.drawUIText sb (sprintf "HP %i/%i" actor.CHP actor.Statistics.MHP) baseX (baseY + 8 + titleSpacing)
        // draw lv
        do UIText.drawUITextRight sb (sprintf "LV. %i" actor.Level) (baseX + pos.Width - 16) (baseY + 8 + titleSpacing)
        // draw hp bar
        let hpBarLoc = new RectangleF (float32 baseX, float32 (baseY + 16 + titleSpacing + textSpacing), float32 (width - 16), float32 12)
        let percHP = float32 actor.CHP / float32 actor.Statistics.MHP
        let hpBarInnerLoc = new RectangleF (hpBarLoc.X + 2.0f, hpBarLoc.Y + 1.0f, (hpBarLoc.Width - 4.0f) * percHP, hpBarLoc.Height - 2.0f)
        do sb.FillRectangle (hpBarLoc, Color.Gray)
        do sb.FillRectangle (hpBarInnerLoc, Color.Green)
        // Draw weapon
        match actor.Weapon with
        | Some weapon -> weapon.DisplayName
        | None -> "No Equip"
        |> fun str -> do UIText.drawUIText sb str baseX (baseY + 24 + titleSpacing + textSpacing + 12)
    