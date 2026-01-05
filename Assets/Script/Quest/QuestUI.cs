using UnityEngine;
using TMPro;

public class QuestUI : MonoBehaviour
{
    public static QuestUI Instance;
    public TextMeshProUGUI questText;

    void Awake()
    {
        Instance = this;
        questText.text = "";
    }

    public void Refresh()
    {
        if (QuestManager.Instance == null)
        {
            questText.text = "";
            return;
        }

        QuestData q = QuestManager.Instance.GetCurrentQuest();

        if (q == null)
        {
            questText.text = "";
            return;
        }

        questText.text = q.description + 
                        " (" + q.currentAmount + "/" + q.targetAmount + ")";
    }
}
