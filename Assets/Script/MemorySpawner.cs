using UnityEngine;

public class MemorySpawner : MonoBehaviour
{
    public static MemorySpawner Instance;

    public float spawnRadius = 1.5f;
    public LayerMask wallLayer;
    public float checkRadius = 0.2f;
    public int maxTry = 15;

    void Awake()
    {
        Instance = this;
    }

    public void SpawnMemoryNearPlayer(GameObject memoryPrefab)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null || memoryPrefab == null) return;

        for (int i = 0; i < maxTry; i++)
        {
            Vector2 offset = Random.insideUnitCircle * spawnRadius;
            Vector2 pos = (Vector2)player.transform.position + offset;

            Collider2D hit = Physics2D.OverlapCircle(pos, checkRadius, wallLayer);

            if (hit == null)
            {
                Instantiate(memoryPrefab, pos, Quaternion.identity);
                return;
            }
        }

        
        Vector2 safePos = (Vector2)player.transform.position + Vector2.right;
        Instantiate(memoryPrefab, safePos, Quaternion.identity);
    }
}
