using System;

public class OrganizationData
{
    public string Id;
    public string Name;
    public int CountryIndex;
    public bool IsAcademy;
    public string LogoFileName;

    public OrganizationData(string id, string name, int countryIndex, bool isAcademy, string logoFileName)
    {
        Id = id;
        Name = name;
        CountryIndex = countryIndex;
        IsAcademy = isAcademy;
        LogoFileName = logoFileName;
    }

    public static OrganizationData FromForm(string name, int countryIndex, bool isAcademy,
        string logoFileName)
    {
        return new OrganizationData(
            Guid.NewGuid().ToString(),
            name?.Trim() ?? string.Empty,
            countryIndex,
            isAcademy,
            logoFileName
        );
    }
}
