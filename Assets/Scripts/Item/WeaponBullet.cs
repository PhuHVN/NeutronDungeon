using UnityEngine;

public class WeaponBullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2f;
    private Vector2 direction;
    private Rigidbody2D rb;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.interpolation = RigidbodyInterpolation2D.None;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        rb.linearVelocity = direction * speed;
        Destroy(gameObject, lifeTime);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Hit Enemy");
        }

        Destroy(gameObject);
    }
}