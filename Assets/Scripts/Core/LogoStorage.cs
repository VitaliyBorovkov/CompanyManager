using System;
using System.IO;

using UnityEngine;

public static class LogoStorage
{
    public static readonly string PersistentLogosDirectory =
        Path.Combine(Application.persistentDataPath, "Logos");

    public static string CopyToPersistent(string logoPath)
    {
        if (!File.Exists(logoPath))
        {
            throw new FileNotFoundException("Logo file not found", logoPath);
        }

        if (!Directory.Exists(PersistentLogosDirectory))
        {
            Directory.CreateDirectory(PersistentLogosDirectory);
        }

        string fileExtension = Path.GetExtension(logoPath).ToLowerInvariant();
        if (fileExtension != ".png" && fileExtension != ".jpg" && fileExtension != ".jpeg")
        {
            throw new InvalidOperationException("Only PNG/JPG image are allowed.");
        }

        string fileName = $"{Guid.NewGuid()}{fileExtension}";
        string destinationPathPersistent = Path.Combine(PersistentLogosDirectory, fileName);
        File.Copy(logoPath, destinationPathPersistent, overwrite: false);

#if UNITY_EDITOR
        string assetsPath = Path.Combine(Application.dataPath, "ImportedLogos");
        if (!Directory.Exists(assetsPath))
        {
            Directory.CreateDirectory(assetsPath);
        }

        string destinationPathInAssets = Path.Combine(assetsPath, fileName);
        File.Copy(logoPath, destinationPathInAssets, overwrite: true);
        Debug.Log($"LogoStorage: Copied to Assets/ImportedLogos/{fileName}");
#endif
        Debug.Log($"LogoStorage: Logo copied to {destinationPathPersistent}");
        return fileName;
    }
}
