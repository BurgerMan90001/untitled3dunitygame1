
using UnityEngine;

/// <summary>
/// <br> For when a variable changes in the ink story. </br>
/// </summary>
[CreateAssetMenu(menuName = "Data/VariableStateHandler")]
public class VariableStateHandler : Data
{
    [Header("Data")]
    [SerializeField] private CombatData _combatData;



    /// <summary>
    /// <br> Handles the logic for when a variable changes in the story. </br>
    /// </summary>
    /// <param name="variableName"></param>
    /// <param name="newValue"></param>
    public void OnVariableChanged(string variableName, object newValue)
    {
        switch (newValue)
        {
            case bool boolValue:
                BoolValueChanged(variableName, boolValue);
                break;
            case string stringValue:
                StringValueChanged(variableName, stringValue);
                break;
            case float floatValue:
                FloatValueChanged(variableName, floatValue);
                break;

            case int intValue:
                IntValueChanged(variableName, intValue);
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
        switch (variableName, newValue)
        {
            case ("battleEntered", true): // if battleEntered variable state has changed and is true
                _combatData.EnterCombat();
                break;

        }

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
