using System.Collections.Generic;

public class OrganizationRepository
{
    private readonly List<OrganizationData> organizations = new List<OrganizationData>();
    public IReadOnlyList<OrganizationData> Organizations => organizations;

    public void Load()
    {
        var saveData = SaveLoadService.Load();
        organizations.Clear();
        organizations.AddRange(saveData.organizations);
    }

    public void Save()
    {
        SaveLoadService.SaveProgress(organizations);
    }

    public void AddOrganization(OrganizationData organization)
    {
        organizations.Add(organization);
        Save();
    }
}
