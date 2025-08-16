using System;

using UnityEngine;
using UnityEngine.UI;

public class ButtonsOnCreateScreenView : MonoBehaviour
{
    public event Action OnUploadClicked;
    public event Action OnSaveClicked;
    public event Action OnNextClicked;

    [SerializeField] private Button uploadButton;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button nextButton;

    private NavigationController navigationController;

    public void Initialize(NavigationController navigation)
    {
        navigationController = navigation;
        uploadButton.onClick.AddListener(() => OnUploadClicked?.Invoke());
        saveButton.onClick.AddListener(() => OnSaveClicked?.Invoke());
        nextButton.onClick.AddListener(() => OnNextClicked?.Invoke());
    }
}
