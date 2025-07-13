using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(menuName = "Input/InputData")]
public class InputData : ScriptableObject 
{
    [SerializeField] private List<ScriptableObject> _inputEvents;
    public bool InputEnabled { get; private set; }


    
    public void ActivateInput(bool active)
    {
        
        foreach (IInputEvent inputEvent in _inputEvents.Cast<IInputEvent>())
        {
            if (inputEvent.Enabled)
            {
                inputEvent.SetActive(false);
            }
            else
            {
                inputEvent.SetActive(true);
            }

        }
        
    }
    
    public void ToggleInput()
    {
        if (InputEnabled)
        {
            ActivateInput(false);
            InputEnabled = false;
        }
        else
        {
            ActivateInput(true);
            InputEnabled = true;
        }
    }
    public void ActivateInputAction(bool active)
    {
        if (active)
        {

        }
        else
        {

        }
    }
}
