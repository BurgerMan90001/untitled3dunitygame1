using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour, PlayerInputActions.IPlayerActions, PlayerInputActions.ICombatActions
{
    [Header("Player Input Objects")]
    [SerializeField] private GameInput _gameCameraPrefab;
    private IGameCamera _gameCamera;
    [SerializeField] private GameInput _playerPrefab;
    private IPlayerMovement _playerMovement;

    [SerializeField] private CombatManager _combatManager;

    [Header("Debug")]
    [SerializeField] private bool _clearInventoryOnEnable = false;

    private bool _interfaceEnabled = false;

    [Header("Events")]
    [SerializeField] private DialogueEvents _dialogueEvents;
    [SerializeField] private CombatEvents _combatEvents;
    [SerializeField] private UserInterfaceEvents _userInterfaceEvents;

    private PlayerInputActions _playerInputActions;
    private PlayerInputActions.PlayerActions _playerActions;
    private PlayerInputActions.CombatActions _combatActions;
    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerActions = _playerInputActions.Player;
        _combatActions = _playerInputActions.Combat;
        _playerActions.AddCallbacks(this);
        _combatActions.AddCallbacks(this);
    }
    private void Start()
    {
        if (_gameCameraPrefab.TryGetComponent(out IGameCamera gameCamera))
        {
            _gameCamera = gameCamera;
        }
        else
        {
            Debug.LogError("The game camera prefab does not have a IGameCamera component.");
        }
        if (_playerPrefab.TryGetComponent(out IPlayerMovement playerMovement))
        {

            _playerMovement = playerMovement;
        }
        else
        {
            Debug.LogError("The game player movement prefab does not have a IPlayerMovement component.");
        }
    }

    private void OnEnable()
    {
        _dialogueEvents.OnChoiceSelected += OnChoiceSelected;
        _dialogueEvents.OnUpdateChoices += OnUpdateChoices;


        _combatEvents.OnEnterCombat += OnEnterCombat;
        _combatEvents.OnExitCombat += OnExitCombat;


        _playerActions.Enable();
    }
    private void OnDestroy()
    {
        _dialogueEvents.OnChoiceSelected -= OnChoiceSelected;
        _dialogueEvents.OnUpdateChoices -= OnUpdateChoices;

        _combatEvents.OnEnterCombat -= OnEnterCombat;
        _combatEvents.OnExitCombat -= OnExitCombat;

        _playerActions.Disable();
    }



    private void OnEnterCombat(CombatUnit _)
    {
        _playerActions.Disable();
        _combatActions.Enable();

    }
    private void OnExitCombat(CombatStates combatState)
    {
        _combatActions.Disable();
        _playerActions.Enable();

    }

    private void OnUpdateChoices(List<string> choices) // discards the list of choice strings. disables input
    {
        _playerActions.Disable();
    }

    private void OnChoiceSelected(int choicesIndex) // discards the chosen int index. enables input
    {
        _playerActions.Enable();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        _playerMovement.OnMove(ctx);
    }
    public void OnLook(InputAction.CallbackContext ctx)
    {
        _gameCamera.OnLook(ctx);
    }

    public void OnPickup(InputAction.CallbackContext ctx)
    {
        _gameCamera.OnPickup(ctx);
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        _playerMovement.OnJump(ctx);
    }

    public void OnCrouch(InputAction.CallbackContext ctx)
    {
        _playerMovement.OnCrouch(ctx);
    }

    public void OnSprint(InputAction.CallbackContext ctx)
    {
        _playerMovement.OnSprint(ctx);
    }

    public void OnOpenInventory(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            if (_interfaceEnabled)
            {
                _userInterfaceEvents.SwitchToUserInterface(UserInterfaceType.HUD);
                _interfaceEnabled = false;

                _playerActions.Move.Enable();
                _playerActions.Look.Enable();

                GameCursor.Lock();

            }
            else
            {
                _userInterfaceEvents.SwitchToUserInterface(UserInterfaceType.Inventory);
                _interfaceEnabled = true;

                _playerActions.Move.Disable();
                _playerActions.Look.Disable();

                GameCursor.Unlock();
            }
        }
    }

    public void OnInteract(InputAction.CallbackContext ctx)
    {
        _gameCamera.OnInteract(ctx);
    }

    public void OnDebug(InputAction.CallbackContext ctx)
    {
        throw new System.NotImplementedException();
    }

    public void OnOpenPauseMenu(InputAction.CallbackContext ctx)
    {
        throw new System.NotImplementedException();
    }

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        _combatManager.OnAttackButton(ctx);
    }
    public void OnBlock(InputAction.CallbackContext ctx)
    {
        _combatManager.OnBlockButton(ctx);
    }

    public void OnLeftClick(InputAction.CallbackContext ctx)
    {
        _gameCamera.OnLeftClick(ctx);
    }
}
