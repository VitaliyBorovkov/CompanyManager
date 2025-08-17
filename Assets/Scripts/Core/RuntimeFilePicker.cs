using UnityEngine;

using SForms = System.Windows.Forms;
public class RuntimeFilePicker : IFilePicker
{
    public string OpenImageFile()
    {
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
        try
        {
            using (var ofd = new SForms.OpenFileDialog())
            {
                ofd.Title = "Select Logo";
                ofd.Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";
                ofd.Multiselect = false;

                if (ofd.ShowDialog() == SForms.DialogResult.OK)
                {
                    return ofd.FileName;
                }
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("RuntimeFilePicker: " + ex.Message);
        }
#endif
        Debug.LogWarning("RuntimeFilePicker: File dialog not available.");
        return null;
    }
}
