

public class RuntimeFilePicker : IFilePicker
{
    public string OpenImageFile()
    {
#if SFB_ENABLED
        var exts = new[] {new SFB.ExtentionFilter("Image", "png", "jpg", "jpeg")};
        var res SFB.StandaloneFileBrowser.OpenFilePanel("Select Logo", "", exts, false);
        return (res != null && res.Length > 0) ? res[0] : null;
#else
        UnityEngine.Debug.LogWarning("RuntimeFilePicker: SFB not installed.");
        return null;
#endif
    }
}
