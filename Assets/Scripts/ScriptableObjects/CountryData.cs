using System;

using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "CountryData", menuName = "Data/Country")]
public class CountryData : ScriptableObject
{
    public string countryName;
    public Sprite countryFlag;

}
