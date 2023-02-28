using Raylib_cs;

namespace ChessGame.Render;

public class PieceSet
{
    static string[] pieces =
    {
        "wk", "wq", "wr", "wb", "wn", "wp",
        "bk", "bq", "br", "bb", "bn", "bp",
    };

    public Dictionary<string, Texture2D> Textures { get; private set; }

    private PieceSet(Dictionary<string, Texture2D> textures)
    {
        Textures = textures;
    }

    public static PieceSet LoadFromDirectory(string directory)
    {
        if (!Directory.Exists(directory))
        {
            throw new Exception($"Directory {directory} not found");
        }

        var files = Directory
            .GetFiles(directory)
            .Where(f => Path.GetExtension(f) == ".png")
            .Select<string, (string Name, string Path)>(f => (Path.GetFileNameWithoutExtension(f), Path.GetFullPath(f)))
            .ToDictionary(t => t.Name, t => t.Path);

        Dictionary<string, Texture2D> textures = new();
        
        foreach (var piece in pieces)
        {
            if (!files.TryGetValue(piece, out var path))
            {
                throw new Exception($"Piece set at {directory} does not have an image for piece {piece}");
            }

            textures[piece] = LoadTexture(path);
        }

        if (textures.Count != pieces.Length)
        {
            var missing = string.Join(", ", pieces.Except(textures.Keys));
            throw new Exception($"Piece set is missing the following pieces, or they failed to load: {missing}");
        }

        return new PieceSet(textures);
    }

    private static Texture2D LoadTexture(string path)
    {
        Image img = Raylib.LoadImage(path);
        Texture2D text = Raylib.LoadTextureFromImage(img);
        Raylib.SetTextureFilter(text, TextureFilter.TEXTURE_FILTER_BILINEAR);
        return text;
    }
}
