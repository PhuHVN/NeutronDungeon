using UnityEngine;

public class MageGirl : BasePlayer
{
    public GameObject dieEffectPrefab;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        if (currentHealth <= 0)
        {
            Instantiate(dieEffectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
