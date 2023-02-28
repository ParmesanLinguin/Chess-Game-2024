using Raylib_cs;

namespace ChessGame.Render;

internal class BoardRenderer : IRenderer
{
    GameResourceManager GameRenderer { get; set; }

    public Texture2D Board { get; private set; }
    public Position BoardPixelSize { get; private set; }

    Color darkColor = Color.BROWN;
    Color lightColor = Color.LIGHTGRAY; 

    public BoardRenderer(GameResourceManager game)
    {
        GameRenderer = game;

        Resize(GameRenderer.Width, GameRenderer.Height);
        GenerateBoard(GameRenderer.Width, GameRenderer.Height);
    }

    public void GenerateBoard(int width, int height)
    {
        RenderTexture2D renderText = Raylib.LoadRenderTexture(width, height);

        BoardPixelSize = new(width, height);

        float squareWidth = width / (float)GameRenderer.BoardWidth;
        float squareHeight = height / (float)GameRenderer.BoardHeight;

        Raylib.BeginTextureMode(renderText);

        for (int bx = 0; bx < GameRenderer.BoardWidth; bx++)
        {
            for (int by = 0; by < GameRenderer.BoardHeight; by++)
            {
                var (x, y) = GameRenderer.BoardSquareToScreenSquare(bx, by);
                Color col = (x + (y % 2)) % 2 == 0 ? darkColor : lightColor;
                Raylib.DrawRectangle((int)(x * squareWidth), (int)(y * squareHeight), (int)squareWidth, (int)squareHeight, col);
            }
        }

        Raylib.EndTextureMode();

        Board = renderText.texture;
    }

    public void Draw(float delta)
    {
        Raylib.DrawTexture(Board, 0, 0, Color.WHITE);
    }

    public void Resize(int width, int height)
    {
        GenerateBoard(width, height);
    }
}
