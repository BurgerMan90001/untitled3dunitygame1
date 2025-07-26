
using UnityEngine;



public class NPCShop : MonoBehaviour, IInteractable
{
    [Header("Shop Settings")]
    [SerializeField] private ShopItemPool _shopItemPool;

    [Header("Data")]


    [SerializeField] private GameInput _gameInput;

    [SerializeField] private UserInterfaceEvents _userInterfaceEvents;


    private ShopData _shopData;
    /*
    private ShopData _shopData;
    private string _shopGuid; // MAYBE

    #region
    /// <summary>
    /// <br> Dependancy injection for npcs. </br>
    /// </summary>
    /// <param name="shopData"></param>
    #endregion
    public void Initialize(ShopData shopData, string shopGuid)
    {
        _shopData = shopData;
        _shopGuid = shopGuid;
    }
    */
    #region
    /// <summary>
    /// <br> Called when the player interacts with the shop npc.</br>
    /// </summary>
    #endregion
    public void Interact(GameObject interactor)
    {
        /*
        if (_shopData.InShop)
        {

        }
        else
        {
            _userInterfaceEvents.SwitchToUserInterface(UserInterfaceType.Shop);

            _gameInput.MovementInput.EnableMovement(false);
            _gameInput.CameraInput.EnableLook(false);

        }
        */


        /*
        if (!_shopData.InShop) // if not in the shop
        {
            _shopData.EnterShop(_shopGuid);


        }
        else // if already in shop, exit shop when interacted with
        {
            _shopData.ExitShop();
        }
        */
    }
    /*
    public void GenerateContents()
    {

        switch (_shopItemPool.ShopType)
        {
            case ShopType.Lower:

                break;
            case ShopType.Middle:

                break;
            case ShopType.Upper:

                break;

            default:
                Debug.LogError("Could not find the shop's type!");
                break;
        }
    }

    */

}

public class ShopManager
{

}
