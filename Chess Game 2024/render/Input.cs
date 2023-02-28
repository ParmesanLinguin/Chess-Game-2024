using ChessGame.Misc;
using Raylib_cs;

namespace ChessGame.Render;

internal class Input
{
    GameResourceManager GameRenderer { get; set; }

    public Input(GameResourceManager gameRenderer)
    {
        GameRenderer = gameRenderer;
    }

    public void Update()
    {
        var (x, y) = Raylib.GetMousePosition().ToPosition();
        var boardPos = GameRenderer.ScreenPositionToScreenSquare(x, y);

        var leftPressed = Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT);
        var rightPressed = Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_RIGHT);

        var leftReleased = Raylib.IsMouseButtonReleased(MouseButton.MOUSE_BUTTON_LEFT);
        var rightReleased = Raylib.IsMouseButtonReleased(MouseButton.MOUSE_BUTTON_RIGHT);

        if (leftPressed && GameRenderer.IsValidSquare(boardPos))
        {

        }
    }
}
