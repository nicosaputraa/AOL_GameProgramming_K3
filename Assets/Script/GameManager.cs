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
        // Membuat slot kosong saat game mulai
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
        // 1. Cek Stack: Apakah barang sudah ada?
        foreach (InventorySlot slot in slots)
        {
            if (slot.isOccupied && slot.itemName == itemName)
            {
                slot.StackItem();
                return; 
            }
        }

        // 2. Cek Kosong: Cari slot baru
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