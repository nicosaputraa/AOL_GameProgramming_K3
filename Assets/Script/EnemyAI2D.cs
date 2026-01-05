using UnityEngine;
using System.Collections;

public class EnemyAI2D : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Animator animator;
    public Rigidbody2D rb;
    public EnemySpawner spawner;

    [Header("Knockback")]
    public float knockbackForce = 4f;
    public float knockbackDuration = 0.15f;

    [Header("Ranges")]
    public float viewRange = 6f;
    public float attackRange = 2.5f;

    [Header("Movement")]
    public float moveSpeed = 2f;

    [Header("Attack")]
    public float attackCooldown = 0.5f;
    // --- TAMBAHAN BARU 1: Variabel Damage Musuh ---
    public float damageToPlayer = 5f; 
    // ----------------------------------------------
    float lastAttackTime;

    [Header("Health")]
    public int maxHealth = 3;
    int currentHealth;
    bool isDead;
    bool isKnockback;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isDead || isKnockback || player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist <= attackRange)
            Attack();
        else if (dist <= viewRange)
            Chase();
        else
            Idle();
    }

    void Chase()
    {
        Vector2 dir = (player.position - transform.position).normalized;
        rb.linearVelocity = dir * moveSpeed;
        animator.SetFloat("Speed", rb.linearVelocity.magnitude);
        Flip(dir.x);
    }

    void Attack()
    {
        rb.linearVelocity = Vector2.zero;
        animator.SetFloat("Speed", 0);

        if (Time.time - lastAttackTime >= attackCooldown)
        {
            animator.SetTrigger("Attack");
            lastAttackTime = Time.time;

            // --- TAMBAHAN BARU 2: Logika Mengurangi Darah Player ---
            // Kita cek apakah player ada script PlayerStats, lalu panggil TakeDamage
            if (player != null)
            {
                PlayerStats stats = player.GetComponent<PlayerStats>();
                if (stats != null)
                {
                    stats.TakeDamage(damageToPlayer);
                }
            }
            // --------------------------------------------------------
        }
    }

    void Idle()
    {
        rb.linearVelocity = Vector2.zero;
        animator.SetFloat("Speed", 0);
    }

    void Flip(float x)
    {
        transform.localScale = new Vector3(x >= 0 ? 1 : -1, 1, 1);
    }

    public void TakeDamage(int dmg)
    {
        if (isDead) return;

        currentHealth -= dmg;
        animator.SetTrigger("hurt");

        if (!isKnockback)
            StartCoroutine(Knockback());

        if (currentHealth <= 0)
            Die();
    }

    IEnumerator Knockback()
    {
        isKnockback = true;
        rb.linearVelocity = Vector2.zero;

        Vector2 dir = (transform.position - player.position).normalized;
        rb.AddForce(dir * knockbackForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(knockbackDuration);

        rb.linearVelocity = Vector2.zero;
        isKnockback = false;
    }

    void Die()
    {
        if (isDead) return;          
        isDead = true;

        rb.linearVelocity = Vector2.zero;
        animator.SetBool("IsDead", true);

        if (spawner) spawner.OnEnemyDied();

        if (QuestManager.Instance != null)
            QuestManager.Instance.AddProgress("Skeleton");

        Destroy(gameObject, 1.2f);
    }
}