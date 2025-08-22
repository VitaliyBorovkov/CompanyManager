using System;

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
    private OrganizationRepository organizationRepository;

    private IFilePicker filePicker;

    private Action onNextHandler;

    private void Awake()
    {
        navigationController = new NavigationController(screenMainMenu, screenCreate, screenChoose);

        //mainMenuView.Initialize(navigationController);
        //createBackButtonView.Initialize(navigationController);
        //buttonsOnCreateScreenView.Initialize(navigationController);
        //chooseBackButtonView.Initialize(navigationController);
        UIInitializer uiInitializer = new UIInitializer();
        uiInitializer.Initialize(navigationController, mainMenuView, createBackButtonView,
            createOrganizationView, buttonsOnCreateScreenView, chooseBackButtonView);
        createOrganizationView.Initialize();
        organizationsListView.Initialize(LogoStorage.PersistentLogosDirectory);

#if UNITY_EDITOR
        filePicker = new EditorFilePicker();
#else
        filePicker = new RuntimeFilePicker();
#endif

        organizationRepository = new OrganizationRepository();
        organizationRepository.Load();

        organizationsListView.Refresh(organizationRepository.Organizations);

        organizationHandlers = new OrganizationHandlers(filePicker, createOrganizationView, formValidator,
             organizationsListView, organizationRepository
        );

        onNextHandler = () => organizationHandlers.HandleNextClicked(navigationController);

        buttonsOnCreateScreenView.OnUploadClicked += organizationHandlers.HandleUploadClicked;
        buttonsOnCreateScreenView.OnSaveClicked += organizationHandlers.HandleSaveClicked;
        buttonsOnCreateScreenView.OnNextClicked += onNextHandler;

        navigationController.Show(WindowType.MainMenu);
        //Debug.Log("AppController: Navigation initialized.");
    }

    private void OnDestroy()
    {
        if (buttonsOnCreateScreenView != null)
        {
            buttonsOnCreateScreenView.OnUploadClicked -= organizationHandlers.HandleUploadClicked;
            buttonsOnCreateScreenView.OnSaveClicked -= organizationHandlers.HandleSaveClicked;
            buttonsOnCreateScreenView.OnNextClicked -= onNextHandler;
        }

        organizationRepository.Save();
    }
}
