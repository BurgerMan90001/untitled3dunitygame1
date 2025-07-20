using System;
using UnityEngine;
using UnityEngine.UIElements;

// PRESS ONCE TO PREPARE, PRESS AGAIN TO CONFIRM MAYBE

// TODO ANIMETOION MAYBE NOT DO SOM MAKE FEEL GOOD\


// W : ATTACK
// D: BLOCK

public class UI_Combat : IUserInterface
{

    private Button _attackButton; // "Button_Attack"
    private Button _blockButton; // "Button_Block"

    private ProgressBar _healthBar; // "ProgressBar_Health"

    private VisualElement _bottomPanelHUD; // BottomPanel_HUD

    public void QueryElements(VisualElement root)
    {
        _bottomPanelHUD = root.Q<VisualElement>("Bottom_Panel_HUD");

        _attackButton = _bottomPanelHUD.Q<Button>("Button_Attack");
        _blockButton = _bottomPanelHUD.Q<Button>("Button_Block");

        _healthBar = _bottomPanelHUD.Q<ProgressBar>("ProgressBar_Health");
    }

    public void Register(VisualElement root)
    {

        _attackButton.AddManipulator(new AttackButtonManipulator(_attackButton));

        
       // _attackButton.d

    }

    public void Unregister()
    {
        
    }
    /*
    private void OnAttackSelected()
    {
        // PLAY READY ATTACK ANIMATION
    }

    private void OnBlockSelected()
    {
        // PLAY READY BLOCK ANIMATION
    }

    */

}

/// <summary>
/// <br> Plays some animations. </br>
/// </summary>
public class AttackButtonManipulator : Manipulator
{

    public AttackButtonManipulator(VisualElement target) 
    {
        this.target = target;
    }

    protected override void RegisterCallbacksOnTarget()
    {
        target.RegisterCallback<FocusInEvent>(FocusIn);
        target.RegisterCallback<FocusOutEvent>(FocusOut);


        target.RegisterCallback<ClickEvent>(Click);
    }

    protected override void UnregisterCallbacksFromTarget()
    {
        target.UnregisterCallback<FocusInEvent>(FocusIn);
        target.UnregisterCallback<FocusOutEvent>(FocusOut);

        target.UnregisterCallback<ClickEvent>(Click);
    }
    private void Click(ClickEvent evt)
    {
        Debug.Log("CLICK");
    }
    private void FocusOut(FocusOutEvent evt)
    {


        // PLAY SUM ELSE
    }

    private void FocusIn(FocusInEvent evt) // when the button is selected
    {
        // PLAY ATTACK READY ANIMATION
    }

    
}
