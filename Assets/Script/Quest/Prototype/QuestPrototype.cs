using UnityEngine;

public class QuestPrototype : MonoBehaviour
{
    bool playerNear;

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            AcceptQuest();
        }
    }

    void AcceptQuest()
    {
        if (QuestManager.Instance == null)
        {
            Debug.LogError("QuestManager not found in scene!");
            return;
        }

        QuestData q = QuestManager.Instance.GetCurrentQuest();
        if (q == null)
        {
            Debug.Log("No more quests.");
            return;
        }

        Debug.Log("Quest accepted: " + q.questName);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            Debug.Log("Player near Goblin. Press E to accept quest.");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            Debug.Log("Player left Goblin.");
        }
    }
}
