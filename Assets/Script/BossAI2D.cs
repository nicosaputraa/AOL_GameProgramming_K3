using UnityEngine;

public class BossAI2D : MonoBehaviour
{
    public Transform player;
    private bool playerInsideRoom = false;
    private bool returningToSpawn = false;
    private Vector2 spawnPosition;
    public float returnSpeed = 3f;

    [Header("Stats")]
    public int maxHP = 200;
    private int currentHP;

    [Header("Movement")]
    public float moveSpeed = 2f;
    public float attackRange = 1.5f;

    [Header("Attack")]
    public float attackCooldown = 2f;
    private float attackTimer;
    private bool isAttacking;
    private bool isDead;

    private Rigidbody2D rb;
    private Animator animator;

    private bool facingRight = true;

    [Header("Detection")]
    public float viewDistance = 6f;
    private bool isChasing;


    void Start()
    {
        spawnPosition = transform.position;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHP = maxHP;
        attackTimer = 0f;

        // SAFETY
        rb.gravityScale = 0;
        rb.freezeRotation = true;
    }

    void Update()   
    {
        if (returningToSpawn)
        {
            ReturnToSpawn();
            return;
        }

        if (!playerInsideRoom)
        {
            StopMoving();
            return;
        }

        if (isDead || player == null)
            return;

        float distance = Vector2.Distance(transform.position, player.position);

        // === DETECTION LOGIC ===
        if (!isChasing && distance <= viewDistance)
            isChasing = true;

        if (isChasing && distance > viewDistance + 2f) // buffer biar ga flicker
            isChasing = false;

        if (!isChasing)
        {
            StopMoving();
            return;
        }

        // === ATTACK COOLDOWN ===
        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;

        if (isAttacking)
            return;

        if (distance > attackRange)
        {
            Chase();
        }
        else if (attackTimer <= 0)
        {
            StartAttack();
        }
    }

    public void SetPlayerInsideRoom(bool inside)
    {
        playerInsideRoom = inside;

        if (!inside)
        {
            isChasing = false;
            isAttacking = false;
            returningToSpawn = true;
            animator.SetBool("IsMoving", true);
        }
    }
    void ReturnToSpawn()
    {
        Vector2 dir = spawnPosition - (Vector2)transform.position;

        if (dir.magnitude < 0.1f)
        {
            returningToSpawn = false;
            StopMoving();
            return;
        }

        rb.linearVelocity = dir.normalized * returnSpeed;
        HandleFacing();
    }


    void Chase()
    {
        animator.SetBool("IsMoving", true);

        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;

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

        animator.ResetTrigger("Attack");
        animator.SetTrigger("Attack");
    }

    // DIPANGGIL DARI ANIMATION EVENT
    public void EndAttack()
    {
        isAttacking = false;
        attackTimer = attackCooldown;
    }

    void HandleFacing()
    {
        if (rb.linearVelocity.x > 0)
            transform.localScale = new Vector3(-1, 1, 1); // kanan
        else if (rb.linearVelocity.x < 0)
            transform.localScale = new Vector3(1, 1, 1);  // kiri
    }


    void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }


    // ===== DAMAGE =====
    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHP -= damage;
        animator.SetTrigger("Hurt");

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        animator.SetBool("IsDead", true);
        rb.linearVelocity = Vector2.zero;
        rb.simulated = false;
    }
}
