using UnityEngine;

public class Boss2 : BossEnemy
{
    [Header("Shooting Pattern Settings")]
    public float spreadAngle = 22f;
    public int normalBulletCount = 2;
    public int enragedBulletCount = 4;

    public override void Shoot()
    {
        if (!bulletPrefab || !player) return;


        int bulletCount = currentHealth <= maxHealth * 0.5f ? enragedBulletCount : normalBulletCount;

        Vector2 dirToPlayer = (player.position - transform.position).normalized;

        float totalSpread = spreadAngle * (bulletCount - 1);
        float startAngle = -totalSpread / 2f;

        float baseAngle = Mathf.Atan2(dirToPlayer.y, dirToPlayer.x) * Mathf.Rad2Deg;

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = baseAngle + startAngle + i * spreadAngle;
            Vector2 shootDir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
            rbBullet.linearVelocity = shootDir * bulletSpeed;
        }
    }
}
