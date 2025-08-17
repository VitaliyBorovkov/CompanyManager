using System.Collections.Generic;
using System.IO;

using UnityEngine;

public class OrganizationHandlers
{
    private readonly IFilePicker filePicker;
    private readonly CreateOrganizationView createOrganizationView;
    private readonly FormValidator formValidator;
    private readonly OrganizationsListView organizationsListView;
    private readonly List<OrganizationData> organizations;
    private string currentLogoFileName;

    public OrganizationHandlers(IFilePicker filePicker, CreateOrganizationView createOrganizationView,
        FormValidator formValidator, OrganizationsListView organizationsListView,
        List<OrganizationData> organizations)
    {
        this.filePicker = filePicker;
        this.createOrganizationView = createOrganizationView;
        this.formValidator = formValidator;
        this.organizationsListView = organizationsListView;
        this.organizations = organizations;
    }

    public void HandleUploadClicked()
    {
        var source = filePicker?.OpenImageFile();
        if (string.IsNullOrEmpty(source))
        {
            Debug.LogWarning("AppController: No file selected.");
            return;
        }

        try
        {
            currentLogoFileName = LogoStorage.CopyToPersistent(source);

            string fullPath = Path.Combine(LogoStorage.PersistentLogosDirectory, currentLogoFileName);
            Texture2D texture2D = TextureLoader.LoadTexture(fullPath);
            createOrganizationView.SetLogoTexture(texture2D);
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"AppController: Error uploading logo: {ex.Message}");
        }
    }

    public void HandleSaveClicked()
    {
        if (!formValidator.Validate())
        {
            Debug.LogWarning("AppController: Validation failed.");
            return;
        }

        string organizationName = createOrganizationView.GetName();
        string countryName = createOrganizationView.GetCountryName();
        string countryFlagID = createOrganizationView.GetCountryID();
        bool isAcademy = createOrganizationView.GetIsAcademy();

        var data = OrganizationData.FromForm(organizationName, countryFlagID, countryName, isAcademy,
            currentLogoFileName);

        organizations.Add(data);

        Debug.Log($"AppController: Organization saved.");

        organizationsListView.Refresh(organizations);

        SaveLoadService.SaveProgress(organizations);
    }

    public void HandleNextClicked(NavigationController navigationController)
    {
        organizationsListView.Refresh(organizations);
        navigationController.ShowChoose();
        Debug.Log("HandleNextClicked: Proceeding to next step...");
    }
}
