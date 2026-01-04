using UnityEngine;
using UnityEngine.UI;

public class MemoryUI : MonoBehaviour {
    public static MemoryUI Instance;
    public GameObject panel;
    public Text titleText, storyText;

    void Awake() {
        Instance = this;
        panel.SetActive(false);
    }

    public void ShowMemory(MemoryFragment m) {
        panel.SetActive(true);
        titleText.text = m.title;
        storyText.text = m.storyText;
        Time.timeScale = 0f;
    }

    public void CloseMemory() {
        panel.SetActive(false);
        Time.timeScale = 1f;
    }
}
