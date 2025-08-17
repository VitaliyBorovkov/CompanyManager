using System.Collections.Generic;
using System.IO;

using UnityEngine;

public class AppController : MonoBehaviour
{
    [SerializeField] private GameObject screenMainMenu;
    [SerializeField] private GameObject screenCreate;
    [SerializeField] private GameObject screenChoose;

    [SerializeField] private MainMenuView mainMenuView;
    [SerializeField] private BackButtonView createBackButtonView;
    [SerializeField] private CreateOrganizationView createOrganizationView;
    [SerializeField] private ButtonsOnCreateScreenView buttonsOnCreateScreenView;
    [SerializeField] private BackButtonView chooseBackButtonView;
    [SerializeField] private OrganizationsListView organizationsListView;
    [SerializeField] private FormValidator formValidator;

    private NavigationController navigationController;

    private IFilePicker filePicker;
    private string currentLogoFileName;

    private readonly List<OrganizationData> organizations = new List<OrganizationData>();

    public IReadOnlyList<OrganizationData> Organizations => organizations;

    private void Awake()
    {
        navigationController = new NavigationController(screenMainMenu, screenCreate, screenChoose);

        mainMenuView.Initialize(navigationController);
        createBackButtonView.Initialize(navigationController);
        createOrganizationView.Initialize();
        buttonsOnCreateScreenView.Initialize(navigationController);
        chooseBackButtonView.Initialize(navigationController);
        organizationsListView.Initialize(LogoStorage.PersistentLogosDirectory);

        buttonsOnCreateScreenView.OnUploadClicked += HandleUploadClicked;
        buttonsOnCreateScreenView.OnSaveClicked += HandleSaveClicked;
        buttonsOnCreateScreenView.OnNextClicked += HandleNextClicked;

#if UNITY_EDITOR
        filePicker = new EditorFilePicker();
#else
        filePicker = new RuntimeFilePicker();
#endif

        navigationController.ShowMain();
        Debug.Log("AppController: Navigation initialized.");
    }

    private void OnDestroy()
    {
        if (buttonsOnCreateScreenView != null)
        {
            buttonsOnCreateScreenView.OnUploadClicked -= HandleUploadClicked;
            buttonsOnCreateScreenView.OnSaveClicked -= HandleSaveClicked;
            buttonsOnCreateScreenView.OnNextClicked -= HandleNextClicked;
        }
    }

    private void HandleUploadClicked()
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
            Debug.Log($"AppController: Logo uploaded successfully: {currentLogoFileName}");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"AppController: Error uploading logo: {ex.Message}");
        }
    }

    private void HandleSaveClicked()
    {
        if (!formValidator.Validate())
        {
            Debug.LogWarning("AppController: Validation failed.");
            return;
        }

        string organizationName = createOrganizationView.GetName();
        string countryName = createOrganizationView.GetCountryName();
        Sprite countryFlag = createOrganizationView.GetCountryFlag();
        bool isAcademy = createOrganizationView.GetIsAcademy();

        var data = OrganizationData.FromForm(organizationName, countryFlag, countryName, isAcademy,
            currentLogoFileName);

        organizations.Add(data);

        Debug.Log($"AppController: Organization saved with name '{organizationName}', " +
            $"country index {createOrganizationView.GetCountryName()}, " +
            $"isAcademy: {isAcademy}, logo: {currentLogoFileName}, total: {organizations.Count}");

        organizationsListView.Refresh(Organizations);
    }

    private void HandleNextClicked()
    {
        organizationsListView.Refresh(Organizations);
        navigationController.ShowChoose();
        Debug.Log("HandleNextClicked: Proceeding to next step...");
    }
}
