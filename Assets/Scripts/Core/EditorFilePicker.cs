#if UNITY_EDITOR
using System.IO;

using UnityEditor;


public class EditorFilePicker : IFilePicker
{
    public string OpenImageFile()
    {
        var path = EditorUtility.OpenFilePanel("Select Logo", "", "png,jpg,jpeg");
        return string.IsNullOrEmpty(path) ? null : path;
    }
}
#endif