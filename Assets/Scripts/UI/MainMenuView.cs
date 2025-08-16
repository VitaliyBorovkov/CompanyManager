using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button createButton;
    [SerializeField] private Button chooseButton;

    private NavigationController navigation;

    public void Initialize(NavigationController navigationController)
    {
        navigation = navigationController;
        createButton.onClick.AddListener(() => navigation.ShowCreate());
        chooseButton.onClick.AddListener(() => navigation.ShowChoose());
    }
}
