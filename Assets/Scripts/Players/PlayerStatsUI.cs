using UnityEngine;
using UnityEngine.UI;
public class PlayerStatsUI : MonoBehaviour
{
    public float maxHealth = 100f;
    public float maxArmor = 100f;
    public float maxEnergy = 100f;

    // Biến lưu giá trị hiện tại
    private float currentHealth;
    private float currentArmor;
    private float currentEnergy;

    public Image healthFill;
    public Image armorFill;
    public Image energyFill;

    void Start()
    {
        currentHealth = maxHealth - 50f;
        currentArmor = maxArmor;
        currentEnergy = maxEnergy;

        UpdateUI();
    }

    void UpdateUI()
    {
        healthFill.fillAmount = currentHealth / maxHealth;
        armorFill.fillAmount = currentArmor / maxArmor;
        energyFill.fillAmount = currentEnergy / maxEnergy;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(10f);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            Heal(10f);
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        UpdateUI();
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UpdateUI();
    }
}
