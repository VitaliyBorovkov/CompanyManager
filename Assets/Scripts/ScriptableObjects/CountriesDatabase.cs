using UnityEngine;

[CreateAssetMenu(fileName = "CountriesDatabase", menuName = "CountriesDatabase")]
public class CountriesDatabase : ScriptableObject
{
    public CountryData[] countries;
}
