using UnityEngine;

public class BasePlayer : MonoBehaviour
{
    public static BasePlayer instance;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 15f;

    [Header("Health")]
    public float maxHealth;
    public float currentHealth;

    [Header("Combat")]
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float shootCooldown = 0.25f;
    public float bulletSpeed = 10f;
    private float shootTimer;

    [Header("Coin")]
    public int currentCoins;

    private Camera mainCam;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 movement;
    private Rigidbody2D rb;

    public PlayerStatsUI playerHUD;

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
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        playerHUD.UpdateHealth(currentHealth, maxHealth);
        playerHUD.UpdateArmor(20, 20);
        playerHUD.UpdateEnergy(20, 20);
    }

    protected virtual void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        float targetSpeed = movement.magnitude;
        float currentSpeed = animator.GetFloat("speed");
        float smoothSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * 10f);
        animator.speed = 0.8f + movement.magnitude * 0.3f;

        animator.SetFloat("speed", smoothSpeed);
        FlipToMouse();
        HandleShoot();
    }

    protected virtual void FixedUpdate()
    {
        Move();
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
        AudioManage.instance?.PlayShootSound();
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

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        // Decrease Health in HUD
        playerHUD.UpdateHealth(currentHealth, maxHealth);

        Debug.Log("Player took " + damage + " damage! Health is now: " + currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0; // Don't let health go below zero
            Debug.Log("Player has died.");

            // --- Add your death logic here ---
            // 1. Play death animation
            // animator.SetTrigger("Die"); 

            // 2. Disable this script so you can't move
            // this.enabled = false; 

            // 3. Destroy the player object after a delay
            // Destroy(gameObject, 2f); 
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (playerHUD == null)
        {
            Debug.LogError("Chưa kéo HUD_Frame vào MageGirl!");
            return;
        }

        if (other.CompareTag("HealthPotion"))
        {
            Debug.Log("Nhặt được bình MÁU!");
            playerHUD.UpdateHealth(currentHealth + 10, maxHealth);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("ArmorPickup"))
        {
            Debug.Log("Nhặt được GIÁP!");
            playerHUD.UpdateArmor(30, 30);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("ManaPotion"))
        {
            Debug.Log("Nhặt được bình MANA!");
            playerHUD.UpdateEnergy(30, 30);
            Destroy(other.gameObject);
        }

        // New weapon
        else if (other.CompareTag("Weapon"))
        {
            Debug.Log("Nhặt được VŨ KHÍ!");

            WeaponItem weapon = other.GetComponent<WeaponItem>();

            if (weapon != null)
            {
                EquipWeapon(weapon.weaponStats);
                Destroy(other.gameObject);
            }
        }
    }

    public void EquipWeapon(WeaponData data)
    {
        if (data == null)
        {
            Debug.LogError("Weapon Data bị rỗng!");
            return;
        }

        this.bulletPrefab = data.newBulletPrefab;
        this.shootCooldown = data.newShootCooldown;
        this.bulletSpeed = data.newBulletSpeed;

        Debug.Log("Đã trang bị vũ khí mới!");
    }

    public void AddCoin(int amount)
    {
        this.currentCoins += amount;
        Debug.Log("Hiện có: " + this.currentCoins + " xu.");
    }
}
