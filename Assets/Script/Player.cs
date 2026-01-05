using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public InventoryManager inventoryManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Heart"))
        {
            Destroy(other.gameObject);
            Debug.Log("+15 HP");
        }
        else if (other.CompareTag("Memory"))
        {
            MemoryData memory = other.GetComponent<MemoryData>();

            if (memory == null)
            {
                Debug.LogError("Memory tidak punya MemoryData!");
                return;
            }

            Debug.Log("Memory Fragment Collected: " + memory.memorySceneName);

            SceneManager.LoadScene(memory.memorySceneName, LoadSceneMode.Additive);

            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Collectible"))
        {
            CollectibleItem item = other.GetComponent<CollectibleItem>();

            if (item == null)
            {
                Debug.LogError("Collectible tidak punya CollectibleItem!");
                return;
            }

            inventoryManager.AddItem(item.itemName, item.icon);
            Destroy(other.gameObject);
            Debug.Log("Collected: " + item.itemName);
        }
    }
}
