using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{

    void Update()
    {
        QuestData q = QuestManager.Instance.GetCurrentQuest();
        if (q == null) return;

        questText.text = q.description;
        progressBar.maxValue = q.targetAmount;
        progressBar.value = q.currentAmount;
    }

    public static QuestUI Instance;
    public Text questText;
    public Slider progressBar;

    void Awake() {
        Instance = this;
    }

    public void Refresh() {
        QuestData q = QuestManager.Instance.GetCurrentQuest();
        if (q == null) return;

        questText.text = q.description;
        progressBar.maxValue = q.targetAmount;
        progressBar.value = q.currentAmount;
    }

}
