public static class FilePickerService
{
    public static IFilePicker Create()
    {
#if UNITY_EDITOR
        return new EditorFilePicker();
#else
       return new RuntimeFilePicker();
#endif
    }
}
