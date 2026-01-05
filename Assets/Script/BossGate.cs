using UnityEngine;

public class BossGate : MonoBehaviour
{
    public GameObject bossPrefab;
    public Transform spawnPoint;

    bool bossSpawned = false;

    void Update()
    {
        if (bossSpawned) return;
        if (QuestManager.Instance == null) return;

        if (QuestManager.Instance.currentQuestIndex >= 3)
        {
            SpawnBoss();
        }
    }

    void SpawnBoss()
    {
        bossSpawned = true;
        Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity);
        Debug.Log("Boss has been spawned!");
    }
}
