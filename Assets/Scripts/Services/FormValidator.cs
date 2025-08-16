using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class FormValidator : MonoBehaviour
{
    [SerializeField] private List<FormFieldValidator> formFieldValidators;

    private List<IValidatable> validatables;

    private void Awake()
    {
        validatables = formFieldValidators.OfType<IValidatable>().ToList();
    }

    public bool Validate()
    {
        bool allValid = true;

        foreach (var v in validatables)
        {
            bool valid = v.IsValid();
            v.FieldChecker().SetError(!valid);

            if (!valid)
            {
                allValid = false;
            }
        }
        return allValid;
    }
}
