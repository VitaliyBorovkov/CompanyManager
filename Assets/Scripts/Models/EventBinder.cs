using System;

public class EventBinder
{
    public void Bind(ButtonsOnCreateScreenView buttonsOnCreateScreenView, OrganizationHandlers organizationHandlers,
        NavigationController navigationController, out Action onNextHandler)
    {
        onNextHandler = () => organizationHandlers.HandleNextClicked(navigationController);

        buttonsOnCreateScreenView.OnUploadClicked += organizationHandlers.HandleUploadClicked;
        buttonsOnCreateScreenView.OnSaveClicked += organizationHandlers.HandleSaveClicked;
        buttonsOnCreateScreenView.OnNextClicked += onNextHandler;
    }

    public void Unbind(ButtonsOnCreateScreenView buttonsOnCreateScreenView, OrganizationHandlers organizationHandlers,
        Action onNextHandler)
    {
        if (buttonsOnCreateScreenView != null)
        {
            buttonsOnCreateScreenView.OnUploadClicked -= organizationHandlers.HandleUploadClicked;
            buttonsOnCreateScreenView.OnSaveClicked -= organizationHandlers.HandleSaveClicked;
            buttonsOnCreateScreenView.OnNextClicked -= onNextHandler;
        }
    }
}
