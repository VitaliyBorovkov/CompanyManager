using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class CreateOrganizationView : MonoBehaviour
{
    [SerializeField] private RawImage logo;
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_Dropdown country;
    [SerializeField] private Toggle academy;
    [SerializeField] private TMP_Text errorText;

    public void Initialize()
    {
        ResetForm();

        if (errorText)
        {
            errorText.text = string.Empty;
        }

        if (nameInput)
        {
            nameInput.onValueChanged.AddListener(OnNameInputChanged);
            nameInput.text = string.Empty;
        }

        if (country != null && country.options.Count == 0)
        {
            country.options.Add(new TMP_Dropdown.OptionData("Select Country"));
            country.options.Add(new TMP_Dropdown.OptionData("USA"));
            country.options.Add(new TMP_Dropdown.OptionData("Canada"));
            country.options.Add(new TMP_Dropdown.OptionData("UK"));
            country.value = 0;
            country.RefreshShownValue();
        }
    }

    private void OnNameInputChanged(string newInput)
    {
        SetError(string.Empty);
    }

    public string GetName() => nameInput.text;
    public string GetCountryName() => country.options[country.value].text;
    public bool GetIsAcademy() => academy.isOn;

    public void SetLogoTexture(Texture2D texture2D)
    {
        logo.texture = texture2D;
    }

    public void ResetForm()
    {
        if (nameInput)
        {
            nameInput.onValueChanged.RemoveListener(OnNameInputChanged);
            nameInput.onValueChanged.AddListener(OnNameInputChanged);
            nameInput.text = string.Empty;
        }

        if (logo)
        {
            logo.texture = null;
        }

        if (academy)
        {
            academy.isOn = false;
        }

        if (errorText)
        {
            errorText.text = string.Empty;
        }

        if (country != null)
        {
            country.value = 0;
            country.RefreshShownValue();
        }
    }

    public void SetError(string message)
    {
        if (errorText)
        {
            errorText.text = message ?? string.Empty;
        }
    }
}
