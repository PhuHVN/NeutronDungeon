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
            BasePlayer player = other.GetComponent<BasePlayer>();
            if (player != null)
            {
                player.TakeDamage(10f);
            }
            Destroy(gameObject);
        }

    }
}
