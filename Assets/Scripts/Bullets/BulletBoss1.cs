using UnityEngine;

public class BulletBoss1 : MonoBehaviour
{
    public float lifetime = 5f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            BasePlayer playerHealth = other.GetComponent<BasePlayer>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1f);
            }
            Destroy(gameObject);
        }

    }
}
