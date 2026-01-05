using UnityEngine;

public class BossRoomTrigger : MonoBehaviour
{
    private BossAI2D boss;

    void Start()
    {
        boss = FindFirstObjectByType<BossAI2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            boss.SetPlayerInsideRoom(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (boss == null) return;   // ⬅ boss sudah mati
        if (other.CompareTag("Player"))
            boss.SetPlayerInsideRoom(false);
    }
}
