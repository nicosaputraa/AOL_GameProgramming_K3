using UnityEngine;
using UnityEngine.UI;

public class QuestNPC : MonoBehaviour {

    public GameObject dialogUI;
    public Text questText;

    bool playerNear;

    void Start() {
        dialogUI.SetActive(false);
    }

    void Update() {
        if (playerNear && Input.GetKeyDown(KeyCode.E)) {
            OpenDialog();
        }
    }

    void OpenDialog() {
        QuestData q = QuestManager.Instance.GetCurrentQuest();
        if (q == null) return;

        questText.text = q.description;
        dialogUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void AcceptQuest() {
        dialogUI.SetActive(false);
        Time.timeScale = 1f;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerNear = true;
            Debug.Log("Player in range of NPC");
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerNear = false;
            dialogUI.SetActive(false);
            Time.timeScale = 1f;
            Debug.Log("Player left NPC range");
        }
    }
}
