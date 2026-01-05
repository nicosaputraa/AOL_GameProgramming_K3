using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public GameObject slotPrefab;   
    public Transform inventoryPanel; 
    public int totalSlots = 10;     

    private List<InventorySlot> slots = new List<InventorySlot>();

    void Start()
    {
        
        for (int i = 0; i < totalSlots; i++)
        {
            GameObject newSlot = Instantiate(slotPrefab, inventoryPanel);
            InventorySlot slotScript = newSlot.GetComponent<InventorySlot>();
            slotScript.ClearSlot();
            slots.Add(slotScript);
        }
    }

    public void AddItem(string itemName, Sprite itemIcon)
    {
        
        foreach (InventorySlot slot in slots)
        {
            if (slot.isOccupied && slot.itemName == itemName)
            {
                slot.StackItem();
                return; 
            }
        }

        
        foreach (InventorySlot slot in slots)
        {
            if (!slot.isOccupied)
            {
                slot.AddItemToSlot(itemName, itemIcon);
                return;
            }
        }
        
        Debug.Log("Inventory Penuh!");
    }
}