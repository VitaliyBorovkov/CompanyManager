using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class CountryOptionUI : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Image flagImage;

    public void Setup(CountryData countryData)
    {
        if (nameText)
        {
            nameText.text = countryData.countryName;
        }

        if (flagImage)
        {
            flagImage.sprite = countryData.countryFlag;
        }
    }
}
