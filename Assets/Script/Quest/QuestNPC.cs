using UnityEngine;

public class QuestNPC : MonoBehaviour
{
    public GameObject chatBubble;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    public void Interact()
    {
        QuestData q = QuestManager.Instance.GetCurrentQuest();
        if (q == null) return;

        chatBubble.SetActive(true);

        if (!q.completed)
        {
            Debug.Log("Quest: " + q.description);
        }
        else
        {
            QuestManager.Instance.NextQuest();
            Debug.Log("Quest Completed!");
        }
    }
}
