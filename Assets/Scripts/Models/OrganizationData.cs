using System;

public class OrganizationData
{
    public string Id;
    public string Name;
    public string CountryName;
    public bool IsAcademy;
    public string LogoFileName;

    public OrganizationData(string id, string name, string countryName, bool isAcademy, string logoFileName)
    {
        Id = id;
        Name = name;
        CountryName = countryName;
        IsAcademy = isAcademy;
        LogoFileName = logoFileName;
    }

    public static OrganizationData FromForm(string name, string countryName, bool isAcademy,
        string logoFileName)
    {
        return new OrganizationData(
            Guid.NewGuid().ToString(),
            name?.Trim() ?? string.Empty,
           countryName,
            isAcademy,
            logoFileName
        );
    }
}
