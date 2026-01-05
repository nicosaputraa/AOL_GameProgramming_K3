using UnityEngine;
using TMPro;
using System.Collections;

public class TypeWriterEffect : MonoBehaviour
{
    public float typingSpeed = 0.05f;
    private TextMeshProUGUI textComponent;
    private string fullText;

    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        fullText = textComponent.text;
        textComponent.text = "";

        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        foreach (char c in fullText)
        {
            textComponent.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
