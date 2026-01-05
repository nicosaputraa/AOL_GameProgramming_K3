using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Image iconImage;       
    public TextMeshProUGUI countText; 

    public string itemName;
    public int count = 0;
    public bool isOccupied = false;

    public void AddItemToSlot(string newItemName, Sprite newIcon)
    {
        itemName = newItemName;
        iconImage.sprite = newIcon;
        iconImage.enabled = true; 
        isOccupied = true;
        count = 1;
        UpdateText();
    }

    public void StackItem()
    {
        count++;
        UpdateText();
    }

    void UpdateText()
    {
        
        countText.text = (count > 1) ? count.ToString() : "";
    }

    public void ClearSlot()
    {
        itemName = "";
        iconImage.sprite = null;
        iconImage.enabled = false;
        countText.text = "";
        isOccupied = false;
        count = 0;
    }
}