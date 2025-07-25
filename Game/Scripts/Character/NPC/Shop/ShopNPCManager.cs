public class ShopNPCManager : NPCManager
{

    /*
    protected override void InitializeNPCS()
    {
        foreach (Transform shopNPC in transform)
        {
            if (shopNPC.gameObject.TryGetComponent(out NPCShop component))
            {
                string guid = GenerateGUID();
                component.Initialize(_shopData, guid);
                component.GenerateContents();
                shopNPC.gameObject.SetActive(true);
            }
            else
            {
                shopNPC.gameObject.SetActive(false);
                Debug.LogWarning("An npc was disabled because they did not have an NPCInteraction component.");
            }
        }
    }
    private string GenerateGUID()
    {
        return Guid.NewGuid().ToString("N");
    }
    */

}
