using UnityEngine;

public class EndingNPCSpawner : MonoBehaviour
{
    public static EndingNPCSpawner Instance;
    public GameObject endingNPCPrefab;

    bool spawned;

    void Awake()
    {
        Instance = this;
    }

    public void SpawnEndingNPC(Vector2 pos)
    {
        if (spawned || endingNPCPrefab == null) return;

        spawned = true;
        Instantiate(endingNPCPrefab, pos, Quaternion.identity);
    }
}
