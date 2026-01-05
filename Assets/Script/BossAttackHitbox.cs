using UnityEngine;

public class BossAttackHitbox : MonoBehaviour
{
    public float damage = 20f;
    public float activeTime = 0.2f;
    public float offsetX = 0.6f;

    Collider2D col;
    Transform boss;

    void Awake()
    {
        boss = transform.parent;
        col = GetComponent<Collider2D>();
        col.enabled = false;
    }

    public void Activate()
    {
        float dir = boss.localScale.x > 0 ? 1 : -1;
        transform.localPosition = new Vector3(offsetX * dir, 0, 0);

        col.enabled = true;
        CancelInvoke();
        Invoke(nameof(Deactivate), activeTime);
    }

    void Deactivate()
    {
        col.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("HIT DETECTED: " + other.name);

        if (!other.CompareTag("Player")) return;

        PlayerStats stats = other.GetComponent<PlayerStats>();
        if (stats != null)
        {
            stats.TakeDamage(damage);
            Debug.Log("PLAYER DAMAGED!");
        }
    }
}
