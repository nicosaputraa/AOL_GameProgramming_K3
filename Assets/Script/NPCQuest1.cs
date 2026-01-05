using UnityEngine;

public class NPCQuest : MonoBehaviour
{
    public int questIndex; 
    bool playerInRange;

    void Update()
    {
        if (!playerInRange) return;
        if (!Input.GetKeyDown(KeyCode.E)) return;

        if (QuestManager.Instance == null) return;

        if (QuestManager.Instance.currentQuestIndex != questIndex)
        {
            Debug.Log("Quest belum tersedia.");
            return;
        }

        QuestManager.Instance.StartQuest();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            playerInRange = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            playerInRange = false;
    }
}
