using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    public Text questText;
    public Slider progressBar;

    void Update()
    {
        QuestData q = QuestManager.Instance.GetCurrentQuest();
        if (q == null) return;

        questText.text = q.description;
        progressBar.maxValue = q.targetAmount;
        progressBar.value = q.currentAmount;
    }
}
