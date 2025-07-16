
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(menuName = "Input/InputData")]
public class InputData : ScriptableObject 
{
    [Header("Data")]
    [SerializeField] private DialogueData _dialogueData;

    [Header("InputEvents")]
    [SerializeField] private List<ScriptableObject> _inputEvents;

    public bool InputEnabled { get; private set; }

    
    private void OnEnable()
    {
        _dialogueData.OnChoiceSelected += (_) => ToggleInput(true); // discards the chosen int index. enables input
        _dialogueData.OnUpdateChoices += (_) => ToggleInput(false); // discards the list of choice strings. disables input
    }
    private void OnDisable()
    {
        _dialogueData.OnChoiceSelected -= (_) => ToggleInput(true);
        _dialogueData.OnUpdateChoices -= (_) => ToggleInput(false); 

    }
    private void ActivateInput(bool active)
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
    #region
    /// <summary>
    /// <br> Toggles all input on or off.</br>
    /// <br> If it's off, toggle on and vise versa.</br>
    /// </summary>
    #endregion
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
    

    public void ToggleCombatInput()
    {

    }
    #region
    /// <summary>
    /// <br> Enable or disable all input based on the active boolean. </br>
    /// </summary>
    /// <param name="active"></param>
    #endregion
    public void ToggleInput(bool active)
    {
        ActivateInput(active);
    }

    /*
    public void ActivateInputAction(bool active)
    {
        if (active)
        {
            
        }
        else
        {

        }
        throw new NotImplementedException();
    }
    */
}

public enum InputMaps
{

}


