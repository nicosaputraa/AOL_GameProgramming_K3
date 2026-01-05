using UnityEngine;

public enum QuestType { Kill, Collect, Talk }

[System.Serializable]
public class QuestData
{
    public string questName;
    public string description;

    public QuestType questType;
    public string targetID;

    public int targetAmount;
    public int currentAmount;
    public bool completed;

    public GameObject memoryPrefab;
}
