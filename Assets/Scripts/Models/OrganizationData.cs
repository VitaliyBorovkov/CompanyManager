using System;

using UnityEngine;

public class OrganizationData
{
    public string Id;
    public string Name;
    public Sprite CountryFlagFileName;
    public string CountryName;
    public bool IsAcademy;
    public string LogoFileName;

    public OrganizationData(string id, string name, Sprite countryFlagFileName, string countryName,
        bool isAcademy, string logoFileName)
    {
        Id = id;
        Name = name;
        CountryFlagFileName = countryFlagFileName;
        CountryName = countryName;
        IsAcademy = isAcademy;
        LogoFileName = logoFileName;
    }

    public static OrganizationData FromForm(string name, Sprite countryFlafFileName, string countryName,
        bool isAcademy, string logoFileName)
    {
        return new OrganizationData(
            Guid.NewGuid().ToString(),
            name?.Trim() ?? string.Empty,
            countryFlafFileName,
           countryName,
            isAcademy,
            logoFileName
        );
    }
}
