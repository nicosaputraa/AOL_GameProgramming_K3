using UnityEngine;

public class EnemyAI2D : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Animator animator;
    public Rigidbody2D rb;
    public EnemySpawner spawner;


    [Header("Ranges")]
    public float viewRange = 6f;
    public float attackRange = 2.5f;

    [Header("Movement")]
    public float moveSpeed = 2f;

    [Header("Attack")]
    public float attackCooldown = 0.5f;
    private float lastAttackTime;


    [Header("Health")]
    public int maxHealth = 3;
    int currentHealth;
    bool isDead = false;

    [Header("Wander")]
    public bool enableWander = true;
    public float wanderRadius = 3f;
    public float wanderSpeed = 1.5f;
    public float wanderDelay = 2f;

    private Vector2 wanderTarget;
    private float wanderTimer;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        currentHealth = maxHealth;

    }

    void Update()
    {
        if (isDead) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            Attack();
        }
        else if (distance <= viewRange)
        {
            Chase();
        }
        else if (enableWander)
        {
            Wander();
        }
        else
        {
            Idle();
        }
    }

    void Wander()
    {
        wanderTimer -= Time.deltaTime;

        if (wanderTimer <= 0f)
        {
            wanderTarget = (Vector2)transform.position + Random.insideUnitCircle * wanderRadius;
            wanderTimer = wanderDelay;
        }

        Vector2 dir = (wanderTarget - (Vector2)transform.position);

        if (dir.magnitude < 0.2f)
        {
            rb.linearVelocity = Vector2.zero;
            animator.SetFloat("Speed", 0);
            return;
        }

        dir = dir.normalized;
        rb.linearVelocity = dir * wanderSpeed;

        animator.SetFloat("Speed", rb.linearVelocity.magnitude);
        Flip(dir.x);
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
        }
    }


    void Idle()
    {
        rb.linearVelocity = Vector2.zero;
        animator.SetFloat("Speed", 0);
    }


    void Flip(float x)
    {
        if (x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player kena serang!");
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        rb.linearVelocity = Vector2.zero;
        animator.SetBool("IsDead", true);

        if (spawner != null)
            spawner.OnEnemyDied();

        Destroy(gameObject, 1.2f);
    }
}
