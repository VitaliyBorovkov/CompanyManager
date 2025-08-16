using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class FormFieldValidator : MonoBehaviour, IValidatable
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private RawImage logo;
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private UIFieldChecker checker;

    public UIFieldChecker FieldChecker()
    {
        return checker;
    }

    public bool IsValid()
    {
        if (inputField != null)
        {
            string text = inputField.text;

            return !string.IsNullOrWhiteSpace(text) && InputValidation.IsLatin(text);
        }

        if (logo != null)
        {
            return logo.texture != null;
        }

        if (dropdown != null)
        {
            return dropdown.value >= 0;
        }

        return true;
    }
}
