namespace ChessGame.Render;

public interface IRenderer
{
    public void Draw(float delta);

    public void Resize(int width, int height);
}
