using System.IO;

using UnityEngine;

public class OrganizationHandlers
{
    private readonly IFilePicker filePicker;
    private readonly CreateOrganizationView createOrganizationView;
    private readonly FormValidator formValidator;
    private readonly OrganizationsListView organizationsListView;
    private readonly OrganizationRepository organizationRepository;
    private string currentLogoFileName;

    public OrganizationHandlers(IFilePicker filePicker, CreateOrganizationView createOrganizationView,
        FormValidator formValidator, OrganizationsListView organizationsListView,
        OrganizationRepository organizationRepository)
    {
        this.filePicker = filePicker;
        this.createOrganizationView = createOrganizationView;
        this.formValidator = formValidator;
        this.organizationsListView = organizationsListView;
        this.organizationRepository = organizationRepository;
    }

    public void HandleUploadClicked()
    {
        var source = filePicker?.OpenImageFile();
        if (string.IsNullOrEmpty(source))
        {
            Debug.LogWarning("HandleUploadClicked: No file selected.");
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
            Debug.LogError($"OrganizationHandlers: Error uploading logo: {ex.Message}");
        }
    }

    public void HandleSaveClicked()
    {
        if (!formValidator.Validate())
        {
            Debug.LogWarning("OrganizationHandlers: Validation failed.");
            return;
        }

        string organizationName = createOrganizationView.GetName();
        string countryName = createOrganizationView.GetCountryName();
        string countryFlagID = createOrganizationView.GetCountryID();
        bool isAcademy = createOrganizationView.GetIsAcademy();

        var data = OrganizationData.FromForm(organizationName, countryFlagID, countryName, isAcademy,
            currentLogoFileName);

        organizationRepository.AddOrganization(data);

        //Debug.Log($"HandleSaveClicked: Organization saved.");

        organizationsListView.Refresh(organizationRepository.Organizations);

        organizationRepository.Save();
    }

    public void HandleNextClicked(NavigationController navigationController)
    {
        organizationsListView.Refresh(organizationRepository.Organizations);
        navigationController.Show(WindowType.ChooseOrganization);
        //Debug.Log("HandleNextClicked: Proceeding to next step...");
    }
}
