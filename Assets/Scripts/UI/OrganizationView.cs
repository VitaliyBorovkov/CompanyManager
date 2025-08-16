using System.IO;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class OrganizationView : MonoBehaviour
{

    [SerializeField] private RawImage logo;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text countryText;
    [SerializeField] private GameObject academyCheck;

    public void Bind(OrganizationData organizationData, string logosDirectory)
    {
        nameText.text = organizationData.Name;
        countryText.text = organizationData.CountryName;
        academyCheck.SetActive(organizationData.IsAcademy);

        var logoPath = Path.Combine(logosDirectory, organizationData.LogoFileName);
        if (File.Exists(logoPath))
        {
            var logoTexture = TextureLoader.LoadTexture(logoPath);
            logo.texture = logoTexture;
        }
        else
        {
            logo.texture = null;
            Debug.LogWarning($"Logo file not found: {logoPath}");
        }
    }
}
