using TMPro;

using UnityEngine;

public class CountryDropdownPopulator : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private CountriesDatabase countriesDatabase;

    private void Start()
    {
        Populate();
    }

    private void Populate()
    {
        dropdown.options.Clear();

        foreach (var country in countriesDatabase.countries)
        {
            var option = new TMP_Dropdown.OptionData(country.countryName, country.countryFlag);
            dropdown.options.Add(option);
        }

        dropdown.RefreshShownValue();
    }
}
