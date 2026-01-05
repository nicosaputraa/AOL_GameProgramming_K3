using UnityEngine;

public class BossAI2D : MonoBehaviour
{
    public string bossID = "Boss";
    public Transform player;

    bool playerInsideRoom = false;
    bool returningToSpawn = false;
    Vector2 spawnPosition;
    public float returnSpeed = 3f;

    [Header("Stats")]
    public int maxHP = 200;
    int currentHP;

    [Header("Movement")]
    public float moveSpeed = 2f;
    public float attackRange = 1.5f;

    [Header("Attack")]
    public float attackCooldown = 2f;
    float attackTimer;
    bool isAttacking;
    bool isDead;

    [Header("Player Damage")]
    public float damageToPlayer = 15f;

    Rigidbody2D rb;
    Animator animator;

    [Header("Detection")]
    public float viewDistance = 6f;
    bool isChasing;

    [Header("Hurt")]
    bool isHurt;
    float hurtTimer;
    public float hurtDuration = 0.25f;
    public float knockbackForce = 4f;

    void Start()
    {
        spawnPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHP = maxHP;
        rb.gravityScale = 0;
        rb.freezeRotation = true;
    }

    void Update()
    {
        if (isDead || player == null) return;

        if (isHurt)
        {
            hurtTimer -= Time.deltaTime;
            if (hurtTimer <= 0) EndHurt();
            return;
        }

        if (!playerInsideRoom)
        {
            StopMoving();
            return;
        }

        float dist = Vector2.Distance(transform.position, player.position);

        if (!isChasing && dist <= viewDistance) isChasing = true;
        if (isChasing && dist > viewDistance + 2f) isChasing = false;
        if (!isChasing) { StopMoving(); return; }

        if (attackTimer > 0) attackTimer -= Time.deltaTime;
        if (isAttacking) return;

        if (dist > attackRange)
            Chase();
        else if (attackTimer <= 0)
            StartAttack();
    }

    public void SetPlayerInsideRoom(bool inside)
    {
        if (isDead) return;

        playerInsideRoom = inside;
        if (!inside)
        {
            isChasing = false;
            isAttacking = false;
        }
    }

    void Chase()
    {
        animator.SetBool("IsMoving", true);
        Vector2 dir = (player.position - transform.position).normalized;
        rb.linearVelocity = dir * moveSpeed;
        HandleFacing();
    }

    void StopMoving()
    {
        rb.linearVelocity = Vector2.zero;
        animator.SetBool("IsMoving", false);
    }

    void StartAttack()
    {
        isAttacking = true;
        StopMoving();
        animator.SetTrigger("Attack");

        Invoke(nameof(DealDamageToPlayer), 0.35f);
    }

    void DealDamageToPlayer()
    {
        if (isDead || player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);
        if (dist > attackRange + 0.2f) return;

        PlayerStats stats = player.GetComponent<PlayerStats>();
        if (stats != null)
            stats.TakeDamage(damageToPlayer);
    }

    public void EndAttack()
    {
        isAttacking = false;
        attackTimer = attackCooldown;
    }

    void HandleFacing()
    {
        if (rb.linearVelocity.x > 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (rb.linearVelocity.x < 0)
            transform.localScale = new Vector3(1, 1, 1);
    }

    public void TakeDamage(int damage)
    {
        if (isDead || isHurt) return;

        currentHP -= damage;

        isAttacking = false;                            
        CancelInvoke(nameof(DealDamageToPlayer));       

        isHurt = true;
        hurtTimer = hurtDuration;
        animator.SetBool("IsHurt", true);

        Vector2 dir = (transform.position - player.position).normalized;
        rb.AddForce(dir * knockbackForce, ForceMode2D.Impulse);

        if (currentHP <= 0)
            Die();
    }

    void EndHurt()
    {
        isHurt = false;
        animator.SetBool("IsHurt", false);
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        animator.SetBool("IsDead", true);
        rb.linearVelocity = Vector2.zero;
        rb.simulated = false;

        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        if (QuestManager.Instance != null)
            QuestManager.Instance.AddProgress(bossID);

        if (EndingNPCSpawner.Instance != null)
            EndingNPCSpawner.Instance.SpawnEndingNPC(transform.position);


        Destroy(gameObject, 1.5f);
    }
}