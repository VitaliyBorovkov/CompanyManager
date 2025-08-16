using UnityEngine;
using UnityEngine.UI;

public class UIFieldChecker : MonoBehaviour
{
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color errorColor = Color.red;

    private Graphic targetGraphic;

    private void Awake()
    {
        targetGraphic = GetComponent<Graphic>();
        if (targetGraphic == null)
        {
            Debug.LogError("UIFieldChecker requires an Image component on the GameObject.");
        }

        ResetHighlight();
    }

    public void SetError(bool isError)
    {
        if (targetGraphic != null)
        {
            targetGraphic.color = isError ? errorColor : normalColor;
        }
    }

    public void ResetHighlight()
    {
        if (targetGraphic != null)
        {
            targetGraphic.color = normalColor;
        }
    }

}
