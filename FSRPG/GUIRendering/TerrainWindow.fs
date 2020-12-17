namespace FSRPG.GUIRendering.Windows

module TerrainWindow =
    open FSRPG.GUIRendering
    open FSRPG
    open Microsoft.Xna.Framework

    let width = 300
    let height = 60
    let right = Config.internalWidth - width - 8
    // Sits in the top right
    let normalPosition = new Rectangle(right, 16, width, height)
    let offsetPosition = new Rectangle(right - width, 16, width, height)

    let draw sb (terrain: TileTerrainType) tilePosition tileIndex (mousePos: Vector2) =
        let pos = if normalPosition.Contains mousePos then offsetPosition else normalPosition
        do UIWindow.drawWindowFromRect sb pos |> ignore
        do UIText.drawUITitle sb terrain.Name (pos.X + 8) (pos.Y + 8)
        do UIText.drawUIText sb (sprintf "%A [%i]" tilePosition tileIndex) (pos.X + 8) (pos.Y + 16 + (UIText.titleHeight ()))

    