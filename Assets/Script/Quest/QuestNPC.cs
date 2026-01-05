using UnityEngine;
using TMPro;

public class QuestNPC : MonoBehaviour
{
    public int questIndex;                      // 0 = skeleton, 1 = boss
    public GameObject dialogUI;                 // panel dialog
    public TextMeshProUGUI questText;           // TextMeshPro quest description
    public string lockedText = "Quest ini belum tersedia.";

    bool playerNear;
    bool dialogOpen;

    void Start()
    {
        if (dialogUI != null)
            dialogUI.SetActive(false);
    }

    void Update()
    {
        // Tutup dialog / accept quest pakai E
        if (dialogOpen && Input.GetKeyDown(KeyCode.E))
        {
            AcceptQuest();
            return;
        }

        if (!playerNear) return;
        if (!Input.GetKeyDown(KeyCode.E)) return;
        if (QuestManager.Instance == null) return;

        // ❌ Quest belum waktunya
        if (QuestManager.Instance.currentQuestIndex != questIndex)
        {
            if (dialogUI != null && questText != null)
            {
                dialogUI.SetActive(true);
                questText.text = lockedText;
                dialogOpen = true;
            }
            return;
        }

        OpenDialog();
    }

    void OpenDialog()
    {
        QuestData q = QuestManager.Instance.quests[questIndex];

        if (dialogUI != null && questText != null)
        {
            questText.text = q.description;
            dialogUI.SetActive(true);
            Time.timeScale = 0f;
            dialogOpen = true;
        }
    }

    public void AcceptQuest()
    {
        if (dialogUI != null)
            dialogUI.SetActive(false);

        Time.timeScale = 1f;
        dialogOpen = false;

        if (QuestManager.Instance != null)
            QuestManager.Instance.StartQuest();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerNear = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            if (dialogUI != null)
                dialogUI.SetActive(false);

            Time.timeScale = 1f;
            dialogOpen = false;
        }
    }
}
