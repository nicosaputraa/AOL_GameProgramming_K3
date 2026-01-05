using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
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
    }
}
