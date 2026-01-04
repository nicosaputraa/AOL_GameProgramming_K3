using UnityEngine;

public class PlayerAttackHitbox : MonoBehaviour
{
    public int damage = 25;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
        {
            other.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
        }
    }
}
