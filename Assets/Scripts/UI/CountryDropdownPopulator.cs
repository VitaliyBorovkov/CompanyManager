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
        //dropdown.itemText = null;

        //var templates = new List<TMP_Dropdown.OptionData>();

        foreach (var country in countriesDatabase.countries)
        {
            var option = new TMP_Dropdown.OptionData(country.countryName, country.countryFlag);
            //templates.Add(option);
            dropdown.options.Add(option);
        }

        //dropdown.AddOptions(templates);
        dropdown.RefreshShownValue();
    }
}
