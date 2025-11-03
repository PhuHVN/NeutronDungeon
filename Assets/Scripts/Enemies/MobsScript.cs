using Unity.VisualScripting;
using UnityEngine;
using static Unity.Cinemachine.IInputAxisOwner.AxisDescriptor;

public class MobsScript : Enemy
{
    // --- Tunable Parameters ---
    [Header("Patrol Settings")]
    [SerializeField] private float patrolMoveSpeed = 2f;
    [SerializeField] private float patrolRadius = 5f;
    [SerializeField] private float patrolWaitTime = 2f;

    [Header("Chase & Attack Settings")]
    [SerializeField] private float chaseSpeed = 4f;
    [SerializeField] private float detectionRadius = 8f;
    [SerializeField] private float attackRadius = 1.5f;

    [Header("Combat Stats")]
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackCooldown = 1.5f;

    [Header("Mobs healthSettings")]
    public float maxHealth = 10f;
    public float currentHealth;
    // --- Private State Variables ---
    private enum EnemyState { Patrolling, Chasing, Attacking }
    private EnemyState currentState;

    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 originPoint;
    private Vector2 patrolTarget;
    private float patrolWaitTimer;
    private float lastAttackTime;

    private float stuckTimer = 0.3f;
    private float maxStuckTime = 3f;


    public WaveController waveController { get; set; }
    private void Awake()
    {
        // Initialize mob health
        currentHealth = maxHealth;
    }
    void Start()
    {
        // 1. Get references
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        // Find the player (Make sure your player has the "Player" tag)
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // 2. Set spawn point and initial state
        originPoint = transform.position;
        currentState = EnemyState.Patrolling;
        SetNewPatrolTarget();
    }

    void Update()
    {
        if (player == null) return; // Do nothing if the player is dead or missing

        // This is the "State Machine"
        switch (currentState)
        {
            case EnemyState.Patrolling:
                HandlePatrolState();
                break;
            case EnemyState.Chasing:
                HandleChaseState();
                break;
            case EnemyState.Attacking:
                HandleAttackState();
                break;
        }
    }

    // --- STATE LOGIC ---

    private void HandlePatrolState()
    {
        // Check for player
        if (Vector2.Distance(transform.position, player.position) < detectionRadius)
        {
            currentState = EnemyState.Chasing;
            stuckTimer = 0f; // Reset stuck timer
            return;
        }

        // Patrol logic
        if (Vector2.Distance(transform.position, patrolTarget) < 0.2f)
        {
            // Reached target, wait
            rb.linearVelocity = Vector2.zero;
            stuckTimer = 0f; // Not stuck
            // animator.SetBool("isMoving", false); // Uncomment if you add 'isMoving'
            patrolWaitTimer += Time.deltaTime;

            if (patrolWaitTimer >= patrolWaitTime)
            {
                SetNewPatrolTarget();
                patrolWaitTimer = 0;
            }
        }
        else
        {
            // Move to target
            MoveTowards(patrolTarget, patrolMoveSpeed);

            // --- STUCK DETECTION ---
            // If we are moving very slowly (or not at all), we might be stuck
            if (rb.linearVelocity.magnitude < 0.1f)
            {
                stuckTimer += Time.deltaTime;
                if (stuckTimer > maxStuckTime)
                {
                    SetNewPatrolTarget(); // Give up and find a new spot
                    stuckTimer = 0f;
                }
            }
            else
            {
                stuckTimer = 0f; // We are moving, so not stuck
            }
            // --- END STUCK DETECTION ---
        }
    }

    private void HandleChaseState()
    {
        // Check ranges
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRadius)
        {
            currentState = EnemyState.Attacking;
            return;
        }

        if (distanceToPlayer > detectionRadius)
        {
            // Player escaped, go back to patrolling
            currentState = EnemyState.Patrolling;
            SetNewPatrolTarget(); // Find a new patrol point
            return;
        }

        // Chase logic
        MoveTowards(player.position, chaseSpeed);
    }

    private void HandleAttackState()
    {
        // Stop moving to attack
        rb.linearVelocity = Vector2.zero;

        // Check if player is still in range
        if (Vector2.Distance(transform.position, player.position) > attackRadius)
        {
            currentState = EnemyState.Chasing;
            return;
        }

        // Check attack cooldown
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
        }
    }

    // --- HELPER FUNCTIONS ---

    private void SetNewPatrolTarget()
    {
        // Find a random point within the patrol radius around the origin point
        float randomX = Random.Range(-patrolRadius, patrolRadius);
        float randomY = Random.Range(-patrolRadius, patrolRadius);
        patrolTarget = originPoint + new Vector2(randomX, randomY);
    }

    private void MoveTowards(Vector2 target, float speed)
    {
        // Move
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        rb.linearVelocity = direction * speed;

        // Flip sprite to face target
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void Attack()
    {
        lastAttackTime = Time.time;
        animator.SetTrigger("Attack"); // Play attack animation

        // Try to find the player's health script and deal damage
      BasePlayer playerHealth = player.GetComponent<BasePlayer>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }

    // --- GIZMOS (For debugging in Scene view) ---
    private void OnDrawGizmosSelected()
    {
        // Draw Patrol Radius
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(originPoint, patrolRadius);

        // Draw Detection Radius
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        // Draw Attack Radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D was called by: " + collision.gameObject.name);
        if (collision.CompareTag("PlayerBullet"))
        {
            Debug.Log("The object was a PlayerBullet!");
            TakeDamage(1f);
        }
    }

    public override void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    public override void Die()
    {
        base.Die();
        // Add death effects here (animations, sounds, etc.)
        //if(waveController != null)
        //{
        //    waveController.EnemyHasDied();
        //}
        Destroy(gameObject);
    }
}
