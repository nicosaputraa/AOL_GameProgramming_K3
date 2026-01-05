using UnityEngine;

public class MemorySpawner : MonoBehaviour
{
    public static MemorySpawner Instance;
    public float spawnRadius = 1.5f;

    void Awake()
    {
        Instance = this;
    }

    public void SpawnMemoryNearPlayer(GameObject memoryPrefab)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null || memoryPrefab == null) return;

        Vector2 offset = Random.insideUnitCircle * spawnRadius;
        Vector2 pos = (Vector2)player.transform.position + offset;

        Instantiate(memoryPrefab, pos, Quaternion.identity);
    }
}
