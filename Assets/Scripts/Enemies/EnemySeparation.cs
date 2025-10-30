using UnityEngine;

public class EnemySeparation : MonoBehaviour
{
    public float separationRadius = 1f;
    public float separationForce = 3f;

    void FixedUpdate()
    {
        Collider2D[] others = Physics2D.OverlapCircleAll(transform.position, separationRadius);
        Vector2 avoidDir = Vector2.zero;

        foreach (var col in others)
        {
            if (col.gameObject != gameObject && col.CompareTag("Enemy"))
            {
                Vector2 diff = (Vector2)(transform.position - col.transform.position);
                avoidDir += diff.normalized / diff.magnitude;
            }
        }

        if (avoidDir != Vector2.zero)
            GetComponent<Rigidbody2D>().AddForce(avoidDir * separationForce);
    }
}
