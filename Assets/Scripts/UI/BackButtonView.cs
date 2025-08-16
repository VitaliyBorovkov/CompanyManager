using UnityEngine;
using UnityEngine.UI;

public class BackButtonView : MonoBehaviour
{
    [SerializeField] private Button backButton;

    private NavigationController navigation;

    public void Initialize(NavigationController navigationController)
    {
        navigation = navigationController;
        backButton.onClick.AddListener(() => navigation.ShowMain());
    }
}
