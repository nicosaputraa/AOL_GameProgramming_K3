using UnityEngine;
using System.Collections.Generic;

public class ManagerPrototype : MonoBehaviour
{
    public static ManagerPrototype Instance;

    public List<QuestData> quests;
    public int currentQuestIndex;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public QuestData GetCurrentQuest()
    {
        if (currentQuestIndex >= quests.Count) return null;
        return quests[currentQuestIndex];
    }

    public void AddProgress(string targetID) {
        QuestData q = GetCurrentQuest();
        if (q == null || q.completed) return;

        if (q.targetID != targetID) return;

        q.currentAmount++;
        Debug.Log("Quest Progress: " + q.currentAmount + "/" + q.targetAmount);

        if (q.currentAmount >= q.targetAmount) {
            q.completed = true;
            Debug.Log("Quest Completed: " + q.questName);
            MemoryManager.Instance.UnlockNextMemory();
        }
    }



    public void NextQuest()
    {
        currentQuestIndex++;
    }
}
