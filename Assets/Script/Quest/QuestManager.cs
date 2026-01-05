using UnityEngine;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    public List<QuestData> quests = new List<QuestData>();
    public int currentQuestIndex = 0;
    public bool questActive = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public QuestData GetCurrentQuest()
    {
        if (!questActive) return null;
        if (currentQuestIndex >= quests.Count) return null;
        return quests[currentQuestIndex];
    }

    public void StartQuest()
    {
        questActive = true;

        if (QuestUI.Instance != null)
            QuestUI.Instance.Refresh();
    }

    public void AddProgress(string targetID)
    {
        if (!questActive) return;

        QuestData q = GetCurrentQuest();
        if (q == null || q.completed) return;
        if (q.targetID != targetID) return;

        q.currentAmount++;

        if (q.currentAmount >= q.targetAmount)
        {
            q.completed = true;

            if (q.memoryPrefab != null && MemorySpawner.Instance != null)
                MemorySpawner.Instance.SpawnMemoryNearPlayer(q.memoryPrefab);

            NextQuest();
            return;
        }

        if (QuestUI.Instance != null)
            QuestUI.Instance.Refresh();
    }

    public void NextQuest()
    {
        currentQuestIndex++;

        if (currentQuestIndex >= quests.Count)
        {
            questActive = false;
            if (QuestUI.Instance != null)
                QuestUI.Instance.Refresh();
            return;
        }

        if (QuestUI.Instance != null)
            QuestUI.Instance.Refresh();
    }
}
