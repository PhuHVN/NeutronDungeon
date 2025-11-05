using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Game/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Header("Thông số sẽ thay đổi")]
    public GameObject newBulletPrefab;
    public float newShootCooldown;
    public float newBulletSpeed;
    internal int price;
}