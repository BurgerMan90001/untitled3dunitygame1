
using Ink.Runtime;
using UnityEngine;

/// <summary>
/// <br> For when a variable changes in the ink story. </br>
/// </summary>
// NOT WORKING TODO
public class VariableStateHandler
{
    private CombatData _combatData;

    private bool _showVariableName = false;

    public VariableStateHandler(CombatData combatData)
    {
        _combatData = combatData;
    }
    /// <summary>
    /// <br> Handles the logic for when a variable changes in the story. </br>
    /// </summary>
    /// <param name="variableName"></param>
    /// <param name="newValue"></param>
    public void OnVariableChanged(string variableName, Ink.Runtime.Object newValue)
    {
        if (_showVariableName)
        {
            Debug.Log(_showVariableName);
            Debug.Log(newValue.GetType().ToString());
        }
        
        
        switch (newValue)
        {
            
            case BoolValue boolValue:
                BoolValueChanged(variableName, boolValue.value);
                break;
            case StringValue stringValue:
                StringValueChanged(variableName, stringValue.value);
                break;
            case FloatValue floatValue:
                FloatValueChanged(variableName, floatValue.value);
                break;

            case IntValue intValue:
                IntValueChanged(variableName, intValue.value);
                break;
            case null:
                Debug.LogError("OnVariableChanged got a null variable. ");
                break;
            default:
                Debug.LogWarning("OnVariableChanged got an unknown variable. ");
                break;

        }

    }


    private void BoolValueChanged(string variableName, bool newValue)
    {
        /*
        switch (variableNamee)
        {
            case ("battleEntered"): // if battleEntered variable state has changed and is true
                _combatData.EnterCombat(newValue);
                
                break;
            default:
                Debug.LogError("Could not find matching variable name.");
                break;
        }
        */

    }
    private void IntValueChanged(string variableName, int newValue)
    {

    }
    private void FloatValueChanged(string variableName, float newValue)
    {

    }

    private void StringValueChanged(string variableName, string newValue)
    {

    }
}
