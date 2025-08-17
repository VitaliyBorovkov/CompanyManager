using System;

[Serializable]
public class OrganizationData
{
    public string Id;
    public string Name;
    public string CountryID;
    public string CountryName;
    public bool IsAcademy;
    public string LogoFileName;

    public OrganizationData(string id, string name, string countryID, string countryName,
        bool isAcademy, string logoFileName)
    {
        Id = id;
        Name = name;
        CountryID = countryID;
        CountryName = countryName;
        IsAcademy = isAcademy;
        LogoFileName = logoFileName;
    }

    public static OrganizationData FromForm(string name, string countryFlagID, string countryName,
        bool isAcademy, string logoFileName)
    {
        return new OrganizationData(
            Guid.NewGuid().ToString(),
            name?.Trim() ?? string.Empty,
            countryFlagID,
           countryName,
            isAcademy,
            logoFileName
        );
    }
}
