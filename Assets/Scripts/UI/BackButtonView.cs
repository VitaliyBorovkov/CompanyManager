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
                    navigation.ShowMain();
                    break;
                case BackTarget.CreateOrganizationScreen:
                    navigation.ShowCreate();
                    break;
                case BackTarget.ChooseOrganizationScreen:
                    navigation.ShowChoose();
                    break;
            }
        });
    }
}
