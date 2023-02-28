// See https://aka.ms/new-console-template for more information

using ChessGame.Render;
using Raylib_cs;

Raylib.InitWindow(800, 800, "Chess Game 2024");
Raylib.InitAudioDevice();

PieceSet ps = PieceSet.LoadFromDirectory(@"C:\Users\Cailan\Documents\Codening\C#\Chess Game 2024\Chess Game 2024\asset\pieces\default\");
SoundSet ss = SoundSet.LoadFromDirectory(@"C:\Users\Cailan\Documents\Codening\C#\Chess Game 2024\Chess Game 2024\asset\sounds\default\");

GameResourceManager renderer = new(8, 8, 800, 800, false, ps);
PieceRenderer pieceRenderer = renderer.PieceRenderer;

pieceRenderer.AddPiece(new Position(0, 0), "wr");
pieceRenderer.AddPiece(new Position(1, 0), "wn");
pieceRenderer.AddPiece(new Position(2, 0), "wb");
pieceRenderer.AddPiece(new Position(3, 0), "wq");
pieceRenderer.AddPiece(new Position(4, 0), "wk");
pieceRenderer.AddPiece(new Position(5, 0), "wb");
pieceRenderer.AddPiece(new Position(6, 0), "wn");
pieceRenderer.AddPiece(new Position(7, 0), "wr");

pieceRenderer.AddPiece(new Position(0, 1), "wp");
pieceRenderer.AddPiece(new Position(1, 1), "wp");
pieceRenderer.AddPiece(new Position(2, 1), "wp");
pieceRenderer.AddPiece(new Position(3, 1), "wp");
pieceRenderer.AddPiece(new Position(4, 1), "wp");
pieceRenderer.AddPiece(new Position(5, 1), "wp");
pieceRenderer.AddPiece(new Position(6, 1), "wp");
pieceRenderer.AddPiece(new Position(7, 1), "wp");

pieceRenderer.AddPiece(new Position(0, 6), "bp");
pieceRenderer.AddPiece(new Position(1, 6), "bp");
pieceRenderer.AddPiece(new Position(2, 6), "bp");
pieceRenderer.AddPiece(new Position(3, 6), "bp");
pieceRenderer.AddPiece(new Position(4, 6), "bp");
pieceRenderer.AddPiece(new Position(5, 6), "bp");
pieceRenderer.AddPiece(new Position(6, 6), "bp");
pieceRenderer.AddPiece(new Position(7, 6), "bp");

pieceRenderer.AddPiece(new Position(0, 7), "br");
pieceRenderer.AddPiece(new Position(1, 7), "bn");
pieceRenderer.AddPiece(new Position(2, 7), "bb");
pieceRenderer.AddPiece(new Position(3, 7), "bq");
pieceRenderer.AddPiece(new Position(4, 7), "bk");
pieceRenderer.AddPiece(new Position(5, 7), "bb");
pieceRenderer.AddPiece(new Position(6, 7), "bn");
pieceRenderer.AddPiece(new Position(7, 7), "br");

Position currentPosition = new Position(0, 0);
Random random = new();

float deltaSum = 0;

while (!Raylib.WindowShouldClose())
{
    Raylib.BeginDrawing();

    Raylib.ClearBackground(Color.BLACK);

    var delta = Raylib.GetFrameTime();

    deltaSum += delta;

    renderer.Draw(delta);

    if (deltaSum >= 0.05f)
    {
        Position pos = new Position(random.Next(8), random.Next(8));
        //Raylib.PlaySoundMulti(ss.CaptureSound);
        pieceRenderer.MovePiece(currentPosition, pos);
        currentPosition = pos;
        deltaSum = 0;
    }

    Raylib.EndDrawing();
}

Raylib.CloseWindow();