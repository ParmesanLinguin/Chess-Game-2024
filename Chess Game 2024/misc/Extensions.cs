using ChessGame.Render;
using Raylib_cs;
using System.Numerics;

namespace ChessGame.Misc;

static class Extensions
{
    public static Rectangle ToRect(this Texture2D r)
    {
        return new Rectangle(0, 0, r.width, r.height);
    }

    public static void Deconstruct(this Vector2 vector, out float x, out float y)
    {
        x = vector.X;
        y = vector.Y;
        return;
    }

    public static Position ToPosition(this Vector2 vector)
    {
        return new Position((int)vector.X, (int)vector.Y);
    }
}
