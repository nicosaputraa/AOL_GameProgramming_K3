using UnityEngine;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

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

    public void AddProgress()
    {
        QuestData q = GetCurrentQuest();
        if (q == null || q.completed) return;

        q.currentAmount++;

        if (q.currentAmount >= q.targetAmount)
        {
            q.completed = true;
        }
    }

    public void NextQuest()
    {
        currentQuestIndex++;
    }
}
