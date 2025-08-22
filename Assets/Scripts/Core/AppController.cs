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
    private EventBinder eventBinder;
    private Action onNextHandler;

    private void Awake()
    {
        navigationController = new NavigationController(screenMainMenu, screenCreate, screenChoose);

        UIInitializer uiInitializer = new UIInitializer();
        uiInitializer.Initialize(navigationController, mainMenuView, createBackButtonView,
            createOrganizationView, buttonsOnCreateScreenView, chooseBackButtonView);

        organizationsListView.Initialize(LogoStorage.PersistentLogosDirectory);

        filePicker = FilePickerService.Create();

        organizationRepository = new OrganizationRepository();
        organizationRepository.Load();

        organizationsListView.Refresh(organizationRepository.Organizations);

        organizationHandlers = new OrganizationHandlers(filePicker, createOrganizationView, formValidator,
             organizationsListView, organizationRepository
        );

        eventBinder = new EventBinder();
        eventBinder.Bind(buttonsOnCreateScreenView, organizationHandlers, navigationController,
            out onNextHandler);

        navigationController.Show(WindowType.MainMenu);
        //Debug.Log("AppController: Navigation initialized.");
    }

    private void OnDestroy()
    {
        eventBinder.Unbind(buttonsOnCreateScreenView, organizationHandlers, onNextHandler);

        organizationRepository.Save();
    }
}
