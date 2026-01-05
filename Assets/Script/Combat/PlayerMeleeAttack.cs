using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMeleeAttack : MonoBehaviour
{
    public int damage = 1;
    public float attackCooldown = 0.4f;
    public float attackDuration = 0.12f;
    public Vector2 boxSize = new Vector2(1.2f, 1f);
    public LayerMask enemyLayer;
    public Animator animator;

    float lastAttackTime;

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        if (Time.time - lastAttackTime < attackCooldown) return;

        lastAttackTime = Time.time;
        StartCoroutine(AttackRoutine());
    }

    System.Collections.IEnumerator AttackRoutine()
    {
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(attackDuration);

        DoDamage();
    }

    public void DoDamage()
    {
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 dir = (mouseWorld - transform.position).normalized;
        Vector2 center = (Vector2)transform.position + dir * 0.7f;

        Collider2D[] hits = Physics2D.OverlapBoxAll(center, boxSize, 0);

        foreach (Collider2D hit in hits)
        {
            EnemyAI2D enemy = hit.GetComponentInParent<EnemyAI2D>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                continue;
            }

            BossAI2D boss = hit.GetComponentInParent<BossAI2D>();
            if (boss != null)
            {
                boss.TakeDamage(damage);
            }
        }
    }



    void OnDrawGizmos()
    {
        float dir = Mathf.Sign(transform.localScale.x);
        Vector2 pos = (Vector2)transform.position + Vector2.right * dir * 0.7f;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos, boxSize);
    }

    void Update()
    {
        FaceMouse();
    }

    void FaceMouse()
    {
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        float dir = mouseWorld.x - transform.position.x;

        if (dir != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = dir > 0 ? 1 : -1;
            transform.localScale = scale;
        }
    }

}