
using UnityEngine;
using TMPro;

public class BuyableItems : MonoBehaviour
{
    [Header("Item Settings")]
    [Tooltip("The ScriptableObject for the weapon this sells")]
    public WeaponData weaponData;

    [Header("Visuals")]
    [Tooltip("Drag the TextMeshPro text for the price tag here")]
    public TextMeshProUGUI priceText;

    private void Start()
    {
        // Set the price tag text when the item is created
        if (priceText != null && weaponData != null)
        {
            priceText.text = weaponData.price.ToString();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 1. Check if the object that entered is the "Player"
        if (collision.CompareTag("Player"))
        {
            // 2. Get the BasePlayer script from the player
            BasePlayer player = collision.GetComponent<BasePlayer>();
            if (player == null) return; // Not a player

            // 3. Check if the player has enough coins
            if (player.currentCoins >= weaponData.price)
            {
                // 4. SUCCESS: Player can afford it!

                // Subtract coins
                player.AddCoin(-weaponData.price);

                // Give the weapon
                player.EquipWeapon(weaponData);

                // (Optional) Play a "purchase successful" sound
                // AudioManage.instance.PlayBuySound();

                // Destroy this item so it can't be bought again
                Destroy(gameObject);
            }
            else
            {
                // 5. FAILED: Not enough money
                Debug.Log("Not enough coins!");

                // (Optional) Play a "cannot afford" sound
                // AudioManage.instance.PlayCantAffordSound();
            }
        }
    }
}
