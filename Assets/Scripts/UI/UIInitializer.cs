public class UIInitializer
{
    public void Initialize(NavigationController navigationController, MainMenuView mainMenuView,
        BackButtonView createBackButtonView, CreateOrganizationView createOrganizationView,
        ButtonsOnCreateScreenView buttonsOnCreateScreenView, BackButtonView chooseBackButtonView)
    {
        mainMenuView.Initialize(navigationController);
        createBackButtonView.Initialize(navigationController);
        createOrganizationView.Initialize();
        buttonsOnCreateScreenView.Initialize(navigationController);
        chooseBackButtonView.Initialize(navigationController);
    }
}
