using ChessGame.Misc;
using Raylib_cs;
using System.Numerics;

namespace ChessGame.Render;

internal class PieceRenderer : IRenderer
{
    public PieceSet PieceSet { get; private set; }
    public GameResourceManager GameRenderer { get; private set; }
    public AnimatedPieceRenderer AnimatedPieceRenderer { get; private set; }

    Dictionary<Position, RenderedPiece> PiecePositions { get; set; }

    float squareWidth;
    float squareHeight;

    public PieceRenderer(PieceSet pieceTextures, GameResourceManager game)
    {
        PieceSet = pieceTextures;
        GameRenderer = game;

        PiecePositions = new();
        AnimatedPieceRenderer = new(this);

        Resize(GameRenderer.Width, GameRenderer.Height);
    }

    public void RemovePiece(Position pos)
    {
        PiecePositions.Remove(pos);
    }

    public void AddPiece(Position pos, string piece)
    {
        PiecePositions[pos] = new RenderedPiece(piece, this);
    }

    public void MovePiece(Position pos, Position to)
    {
        if (PiecePositions.TryGetValue(pos, out var piece))
        {
            PiecePositions.Remove(pos);
            PiecePositions[to] = piece;

            AnimatedPieceRenderer.Animate(piece, pos, to);
        }
    }

    public void Draw(float delta)
    {
        foreach (var (bpos, piece) in PiecePositions)
        {
            var pos = GameRenderer.BoardSquareToScreenSquare(bpos);
            if (piece.Owner != this) { continue; }
            var texture = PieceSet.Textures[piece.PieceType];
            Raylib.DrawTexturePro(texture, texture.ToRect(), new Rectangle(pos.X * squareWidth, pos.Y * squareHeight, squareWidth, squareHeight), Vector2.Zero, 0f, Color.WHITE);
        }

        AnimatedPieceRenderer.Draw(delta);
    }

    public void Resize(int width, int height)
    {
        squareWidth = GameRenderer.Width / (float)GameRenderer.BoardWidth;
        squareHeight = GameRenderer.Height / (float)GameRenderer.BoardHeight;
    }
}

record class RenderedPiece 
{
    public string PieceType { get; set; } 
    public IRenderer Owner { get; set; }

    public RenderedPiece(string type, IRenderer owner)
    {
        PieceType = type;
        Owner = owner;
    }
}