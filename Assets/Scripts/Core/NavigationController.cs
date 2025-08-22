using System.Collections.Generic;

using UnityEngine;

public class NavigationController
{
    private readonly Dictionary<WindowType, GameObject> windows;

    public NavigationController(GameObject mainMenu, GameObject createOrganization, GameObject chooseOrganization)
    {
        windows = new Dictionary<WindowType, GameObject>
        {
            { WindowType.MainMenu, mainMenu },
            { WindowType.CreateOrganization, createOrganization },
            { WindowType.ChooseOrganization, chooseOrganization }
        };
    }

    public void Show(WindowType windowType)
    {
        foreach (var window in windows)
        {
            window.Value.SetActive(window.Key == windowType);
        }
    }
}
