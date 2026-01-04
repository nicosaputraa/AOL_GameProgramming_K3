using UnityEngine;

public class BossUnlocker : MonoBehaviour
{
    public GameObject bossPrefab;
    public Transform spawnPoint;
    private bool spawned;

    void Update()
    {
        QuestData q = QuestManager.Instance.GetCurrentQuest();

        if (!spawned && q != null && q.questName == "Defeat Boss" && q.completed)
        {
            Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity);
            spawned = true;
        }
    }
}
