using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action onDeath;


    public virtual void TakeDamage(float dmg)
    {

    }

    public virtual void Die()
    {

    }
}
