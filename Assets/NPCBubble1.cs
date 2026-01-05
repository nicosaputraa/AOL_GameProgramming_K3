using UnityEngine;
using TMPro;
using System.Collections;

public class NPCBubble1 : MonoBehaviour
{
    public GameObject bubbleCanvas;
    public TextMeshProUGUI bubbleText;
    public string[] messages;
    public float typingSpeed = 0.03f;

    bool playerNear;
    bool isTyping;
    int index;

    void Start()
    {
        bubbleCanvas.SetActive(false);
    }

    void Update()
    {
        if (!playerNear) return;

        bubbleCanvas.transform.forward = Camera.main.transform.forward;

        if (Input.GetKeyDown(KeyCode.E) && !isTyping)
        {
            bubbleCanvas.SetActive(true);
            StartCoroutine(TypeText());
        }
    }

    IEnumerator TypeText()
    {
        isTyping = true;
        bubbleText.text = "";

        foreach (char c in messages[index])
        {
            bubbleText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        index = (index + 1) % messages.Length;
        isTyping = false;
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
            bubbleCanvas.SetActive(false);
            bubbleText.text = "";
            index = 0;
        }
    }
}
