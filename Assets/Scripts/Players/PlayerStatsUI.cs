using UnityEngine;
using UnityEngine.UI;
public class PlayerStatsUI : MonoBehaviour
{
    public Image healthFill;
    public Image armorFill;
    public Image energyFill;

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        if (healthFill != null && maxHealth > 0)
        {
            healthFill.fillAmount = currentHealth / maxHealth;
        }
    }

    public void UpdateArmor(float currentArmor, float maxArmor)
    {
        if (armorFill != null && maxArmor > 0)
        {
            armorFill.fillAmount = currentArmor / maxArmor;
        }
    }

    public void UpdateEnergy(float currentEnergy, float maxEnergy)
    {
        if (energyFill != null && maxEnergy > 0)
        {
            energyFill.fillAmount = currentEnergy / maxEnergy;
        }
    }
}
