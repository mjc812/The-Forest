using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<InventoryPanelSlotsRowSlot> inventoryPanelSlotsRowSlots = new List<InventoryPanelSlotsRowSlot>(36);
    private int inventorySize = 36;
    private Dictionary<int, List<int>> itemIndex;

    private void Awake()
    {
        GameObject[] slotGameObjects = GameObject.FindGameObjectsWithTag("InventoryPanelSlotsRowSlot");
        foreach (GameObject slotGameObject in slotGameObjects)
        {
            inventoryPanelSlotsRowSlots.Add(slotGameObject.GetComponent<InventoryPanelSlotsRowSlot>());
        }
        itemIndex = new Dictionary<int, List<int>>();
    }

    public bool AddItem(GameObject consumableGameObject, Consumable consumable)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            InventoryPanelSlotsRowSlot inventoryPanelSlotsRowSlot = inventoryPanelSlotsRowSlots[i];
            if (inventoryPanelSlotsRowSlot.AddItem(consumableGameObject, consumable, 1))
            {
                return true;
            }
        }
        return false;
    }
}
