namespace ChessGame.Render;

internal class DraggedPieceRenderer
{
    GameResourceManager GameRenderer { get; set; }
    PieceRenderer PieceRenderer { get; set; }
    PieceSet PieceSet { get; set; }

    public DraggedPieceRenderer(GameResourceManager gameRenderer)
    {
        GameRenderer = gameRenderer;
        PieceRenderer = gameRenderer.PieceRenderer;
        PieceSet = gameRenderer.PieceRenderer.PieceSet;
    }
}
