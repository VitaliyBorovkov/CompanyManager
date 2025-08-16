using System.IO;

using UnityEngine;

public class TextureLoader
{
    public static Texture2D LoadTexture(string path)
    {
        Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
        var bytes = File.ReadAllBytes(path);
        if (!texture.LoadImage(bytes))
        {
            Debug.LogWarning("TextureLoader: LoadImage failed.");
        }
        return texture;
    }
}
