using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingNPC : MonoBehaviour
{
    bool playerNear;

    void Update()
    {
        if (!playerNear) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("EndingScene");
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            playerNear = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            playerNear = false;
    }
}
