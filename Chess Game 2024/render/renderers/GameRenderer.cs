namespace ChessGame.Render;

internal class GameResourceManager : IRenderer
{
    public BoardRenderer BoardRenderer { get; private set; }
    public PieceRenderer PieceRenderer { get; private set; }
    public HighlightRenderer HighlightRenderer { get; private set; }
    public Input Input { get; private set; }

    public int BoardWidth { get; private set; }
    public int BoardHeight { get; private set; }

    public int Width { get; private set; }
    public int Height { get; private set; }
    public bool Flipped { get; private set; }

    public GameResourceManager(int boardWidth, int boardHeight, int width, int height, bool flipped, PieceSet ps)
    {
        BoardWidth = boardWidth;
        BoardHeight = boardHeight;

        Width = width;
        Height = height;
        Flipped = flipped;

        Input = new(this);
        BoardRenderer = new(this);
        PieceRenderer = new(ps, this);
    }

    public void Draw(float delta)
    {
        Input.Update();

        BoardRenderer.Draw(delta);
        PieceRenderer.Draw(delta);
    }

    public void Resize(int width, int height)
    {
        Width = width;
        Height = height;

        BoardRenderer.Resize(width, height);
        PieceRenderer.Resize(width, height);
    }

    public Position BoardSquareToScreenSquare(Position pos)
    {
        return BoardSquareToScreenSquare(pos.X, pos.Y);
    }

    public Position BoardSquareToScreenSquare(int x, int y)
    {
        if (!Flipped) return new Position(x, BoardHeight - y - 1);
        else return new Position(BoardWidth - x - 1, y);
    }

    public Position ScreenPositionToScreenSquare(int x, int y)
    {
        int sx = (int)((Width / (float)BoardWidth) * x);
        int sy = (int)((Height / (float)BoardHeight) * x);

        return BoardSquareToScreenSquare(sx, sy);
    }

    public bool IsValidSquare(Position pos)
    {
        return IsValidSquare(pos.X, pos.Y);
    }

    public bool IsValidSquare(int x, int y)
    {
        return x >= 0 && x < BoardWidth && y >= 0 && y < BoardHeight;
    }
}
