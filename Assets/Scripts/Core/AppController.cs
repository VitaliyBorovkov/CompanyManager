using System.Collections.Generic;

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
    private OrganizationHandlers organizationHandlers;

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



#if UNITY_EDITOR
        filePicker = new EditorFilePicker();
#else
        filePicker = new RuntimeFilePicker();
#endif
        var saveData = SaveLoadService.Load();
        organizations.Clear();
        organizations.AddRange(saveData.organizations);

        organizationsListView.Refresh(Organizations);

        organizationHandlers = new OrganizationHandlers(filePicker, createOrganizationView, formValidator,
             organizationsListView, organizations
        );

        buttonsOnCreateScreenView.OnUploadClicked += organizationHandlers.HandleUploadClicked;
        buttonsOnCreateScreenView.OnSaveClicked += organizationHandlers.HandleSaveClicked;
        buttonsOnCreateScreenView.OnNextClicked += () => organizationHandlers.HandleNextClicked(navigationController);

        navigationController.ShowMain();
        Debug.Log("AppController: Navigation initialized.");
    }

    private void OnDestroy()
    {
        if (buttonsOnCreateScreenView != null)
        {
            buttonsOnCreateScreenView.OnUploadClicked -= organizationHandlers.HandleUploadClicked;
            buttonsOnCreateScreenView.OnSaveClicked -= organizationHandlers.HandleSaveClicked;
            buttonsOnCreateScreenView.OnNextClicked -= () => organizationHandlers.HandleNextClicked(navigationController); ;
        }

        SaveLoadService.SaveProgress(organizations);
    }
}
