using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    public static BasePlayer instance;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Health")]
    public float maxHealth;
    public float currentHealth;

    [Header("Combat")]
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float shootCooldown = 0.25f;
    public float bulletSpeed = 10f;
    private float shootTimer;

    private Camera mainCam;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 movement;
    private Rigidbody2D rb;

    protected virtual void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        mainCam = Camera.main;
    }

    protected virtual void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("speed", Mathf.Abs(movement.sqrMagnitude));

        FlipToMouse();
        Move();
        HandleShoot();
    }

    private void Move()
    {
        rb.linearVelocity = movement.normalized * moveSpeed;
    }

    private void HandleShoot()
    {
        if (Input.GetMouseButton(0))
        {
            shootTimer += Time.deltaTime;
            if (shootTimer >= shootCooldown)
            {
                Shoot();
                shootTimer = 0f;
            }
        }
    }

    protected virtual void Shoot()
    {
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDir = (mousePos - firePoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<MageBuller1>().SetDirection(shootDir);

        Rigidbody2D rb2d = bullet.GetComponent<Rigidbody2D>();
        rb2d.linearVelocity = shootDir * bulletSpeed;

        float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(0f, 0f, angle);
        bullet.transform.rotation = firePoint.rotation;
    }

    void FlipToMouse()
    {
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        bool flip = mousePos.x < transform.position.x;
        spriteRenderer.flipX = flip;

        Vector2 dir = (mousePos - firePoint.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
