using System.Collections;
using UnityEngine;

public class BossEnemy : Enemy
{
    [Header("References")]
    public Transform player;
    public GameObject bulletPrefab;

    [Header("Attack Settings")]
    public float fireRate = 1.5f;
    public float bulletSpeed = 10f;
    public float moveSpeed = 2f;

    [Header("Movement Range")]
    public Vector2 moveAreaMin = new Vector2(-13, 20);
    public Vector2 moveAreaMax = new Vector2(10, 33);
    public float changeDirInterval = 2f;

    [Header("State")]
    private float nextFireTime;
    //private bool hasSplit = false;
    private Rigidbody2D rb;
    private Vector2 moveDir;
    private float nextChangeDirTime;

    [Header("Boss healthSettings")]
    public float maxHealth = 10f;
    public float currentHealth;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;

        if (!player)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj)
                player = playerObj.transform;
        }

        PickNewDirection();
    }

    public virtual void Update()
    {
        if (!player) return;
        HandleMovement();
        HandleShooting();
    }

    void HandleMovement()
    {
        if (Time.time >= nextChangeDirTime)
        {
            PickNewDirection();
        }

        Vector2 pos = transform.position;

        if (pos.x < moveAreaMin.x || pos.x > moveAreaMax.x)
        {
            moveDir.x = -moveDir.x;
            pos.x = Mathf.Clamp(pos.x, moveAreaMin.x, moveAreaMax.x);
            PickNewDirection(true);
        }

        if (pos.y < moveAreaMin.y || pos.y > moveAreaMax.y)
        {
            moveDir.y = -moveDir.y;
            pos.y = Mathf.Clamp(pos.y, moveAreaMin.y, moveAreaMax.y);
            PickNewDirection(true);
        }

        rb.linearVelocity = moveDir * moveSpeed;
    }

    void PickNewDirection(bool immediate = false)
    {
        moveDir = Random.insideUnitCircle.normalized;
        nextChangeDirTime = Time.time + (immediate ? 1f : changeDirInterval);
    }

    void HandleShooting()
    {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    public virtual void Shoot()
    {
        if (!bulletPrefab || !player) return;

        Vector2 dir = (player.position - transform.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().linearVelocity = dir * bulletSpeed;
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
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
        Destroy(gameObject);
    }
}
