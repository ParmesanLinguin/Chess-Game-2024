using Raylib_cs;
using System.IO;

namespace ChessGame.Render;

internal class SoundSet
{
    public Sound MoveSound { get; set; }

    public Sound CaptureSound { get; set; }

    private SoundSet(Sound moveSound, Sound captureSound)
    {
        MoveSound = moveSound;
        CaptureSound = captureSound;
    }

    public static SoundSet LoadFromDirectory(string directory)
    {
        if (!Directory.Exists(directory))
        {
            throw new Exception($"Directory {directory} not found");
        }

        var files = Directory
            .GetFiles(directory)
            .Where(f => Path.GetExtension(f) == ".ogg")
            .Select<string, (string Name, string Path)>(f => (Path.GetFileNameWithoutExtension(f), Path.GetFullPath(f)))
            .ToDictionary(t => t.Name, t => t.Path);

        if (!files.TryGetValue("move", out var movePath))
        {
            throw new Exception($"Missing resource move.ogg");
        }

        if (!files.TryGetValue("capture", out var capturePath))
        {
            throw new Exception($"Missing resource capture.ogg");
        }

        var moveSound = Raylib.LoadSound(movePath);
        var captureSound = Raylib.LoadSound(capturePath);

        return new SoundSet(moveSound, captureSound);
    }
}
