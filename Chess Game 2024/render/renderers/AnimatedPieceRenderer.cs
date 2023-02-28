using ChessGame.Misc;
using Raylib_cs;
using System.Numerics;

namespace ChessGame.Render;
internal class AnimatedPieceRenderer : IRenderer
{
    GameResourceManager GameRenderer { get; set; }
    PieceRenderer PieceRenderer { get; set; }
    PieceSet PieceSet { get; set; }

    List<AnimatedPiece> Animations;

    const float AnimationDuration = 0.5f;

    float squareWidth;
    float squareHeight;

    public AnimatedPieceRenderer(PieceRenderer pieceRenderer)
    {
        GameRenderer = pieceRenderer.GameRenderer;
        PieceRenderer = pieceRenderer;
        PieceSet = pieceRenderer.PieceSet;

        Animations = new();

        Resize(GameRenderer.Width, GameRenderer.Height);
    }

    public void Animate(RenderedPiece piece, Position start, Position end)
    {
        var prev = piece.Owner;
        piece.Owner = this;
        Animations.Add(new AnimatedPiece(piece, prev, start, end, 0f));
    }

    public void StopAllAnimations()
    {
        foreach (var piece in Animations)
        {
            piece.RenderedPiece.Owner = piece.PreviousOwner;
        }

        Animations.Clear();
    }

    public void Draw(float delta)
    {
        UpdateAnimationStates(delta);

        foreach (var an in Animations)
        {
            var bStart = GameRenderer.BoardSquareToScreenSquare(an.Start);
            var bEnd = GameRenderer.BoardSquareToScreenSquare(an.End);
            float x = Interp(bStart.X * squareWidth, bEnd.X * squareWidth, an.Progress);
            float y = Interp(bStart.Y * squareHeight, bEnd.Y * squareHeight, an.Progress);

            var texture = PieceSet.Textures[an.RenderedPiece.PieceType];
            Raylib.DrawTexturePro(texture, texture.ToRect(), new Rectangle(x, y, squareWidth, squareHeight), Vector2.Zero, 0f, Color.WHITE);
        }
    }

    public void Resize(int width, int height)
    {
        squareWidth = width / (float)GameRenderer.BoardWidth;
        squareHeight = height / (float)GameRenderer.BoardHeight;
    }

    private void UpdateAnimationStates(float delta)
    {
        for (int i = Animations.Count - 1; i >= 0; i--)
        {
            var an = Animations[i];
            if (an.Progress == 1.0f) { continue; }
            var prog = an.Progress + (delta / AnimationDuration);
            if (prog > 1f)
            {
                Animations.RemoveAt(i);
                an.RenderedPiece.Owner = an.PreviousOwner;
            }
            else
            {
                an.Progress = prog;
                Animations[i] = an;
            }
        }
    }

    float Interp(float a, float b, float t)
    {
        float sq = t * t;
        float pr = t * t / (2f * (sq - t) + 1f);
        return a + (b - a) * pr;
    }

    record struct AnimatedPiece(RenderedPiece RenderedPiece, IRenderer PreviousOwner, Position Start, Position End, float Progress) { }
}
