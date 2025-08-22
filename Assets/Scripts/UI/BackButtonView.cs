using UnityEngine;
using UnityEngine.UI;

public class BackButtonView : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private BackTarget backTarget;

    private NavigationController navigation;

    public void Initialize(NavigationController navigationController)
    {
        navigation = navigationController;
        backButton.onClick.AddListener(() =>
        {
            switch (backTarget)
            {
                case BackTarget.MainMenuScreen:
                    navigation.Show(WindowType.MainMenu);
                    break;
                case BackTarget.CreateOrganizationScreen:
                    navigation.Show(WindowType.CreateOrganization);
                    break;
                case BackTarget.ChooseOrganizationScreen:
                    navigation.Show(WindowType.ChooseOrganization);
                    break;
            }
        });
    }
}
