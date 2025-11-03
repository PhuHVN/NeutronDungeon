using UnityEngine;
using System; // <-- 1. YOU MUST ADD THIS LINE FOR "Action"

public abstract class Enemy : MonoBehaviour
{
    // --- 2. ADD THIS EVENT ---
    // This is the event your WaveController subscribes to.
    public event Action onDeath;


    // 3. YOUR "Die" FUNCTION MUST BE "virtual"
    // And it MUST fire the onDeath event.
    public virtual void Die()
    {
        // Fire the event so the WaveController knows this enemy died
        if (onDeath != null)
        {
            onDeath();
        }
    }

    // You already have this, it's just here for completeness.
    public abstract void TakeDamage(float dmg);
}