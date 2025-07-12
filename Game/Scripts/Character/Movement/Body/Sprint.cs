

using System;
using UnityEngine;
#region
/// <summary>
/// Handles sprinting mechanics for the player, including stamina management and speed adjustments.
/// </summary>
#endregion
public class Sprint
{
    private GeneralStats _stats;

    public Action<float> StaminaChanged; 
    public bool ButtonHeld { get ; private set; }

    public Sprint(GeneralStats stats)
    {
        _stats = stats;
    }
    
    public void Run(MovementStateManager MovementStateManager)
    {

        MovementStateManager.SetMovementState(MovementStates.Running);
        ButtonHeld = true;


    }
    public void CancelRun(MovementStateManager movementStateManager, bool isGrounded)
    {
        if (isGrounded) // don't change from in air movement state to walking while in air
        {
            movementStateManager.SetMovementState(MovementStates.Walking); 
        }
        ButtonHeld = false;



    }
    public void UseStamina(MovementStateManager MovementStateManager, float sprintStaminaCost, bool isGrounded)
    {

        _stats.Stamina -= sprintStaminaCost * Time.fixedDeltaTime;

        if (_stats.Stamina <= 0f)
        {
            _stats.Stamina = 0f;
            CancelRun(MovementStateManager, isGrounded);
        }
    }

    public void RegenerateStamina(float staminaRegenRate)
    {
        _stats.Stamina += staminaRegenRate * Time.fixedDeltaTime;


        if (_stats.Stamina >= _stats.MaxStamina)
        {

            _stats.Stamina = _stats.MaxStamina;

        }
    
    }
    
    public void UpdateStamina(MovementStateManager movementStateManager, float sprintStaminaCost, float staminaRegenRate, bool isGrounded)
    {
        if (ButtonHeld && isGrounded)
        {
            
            UseStamina(movementStateManager, sprintStaminaCost, isGrounded); // use stamina and regenerate are called each frame
        }
        else if (_stats.Stamina != _stats.MaxStamina)
        {

            RegenerateStamina(staminaRegenRate);
        }

    }


}
