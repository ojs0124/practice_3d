using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void TakePhysicalDamage(int damageAmount);
}

public class PlayerCondition : MonoBehaviour, IDamagable
{
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } }
    Condition hunger { get { return uiCondition.hunger; } }
    Condition stamina { get { return uiCondition.stamina; } }

    public event Action OnTakeDamage;

    public float reductionHealthByHunger;

    private void Update()
    {
        hunger.Sub(hunger.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if (hunger.currentValue <= 0.0f)
        {
            health.Sub(reductionHealthByHunger * Time.deltaTime);
        }

        if (health.currentValue <= 0.0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Eat(float amount)
    {
        hunger.Add(amount);
    }

    public void Die()
    {
        Debug.Log("Player Dead");
    }

    public void TakePhysicalDamage(int damageAmount)
    {
        health.Sub(damageAmount);
        OnTakeDamage?.Invoke();
    }

    public bool UseStamina(float amount)
    {
        if (stamina.currentValue - amount < 0)
        {
            return false;
        }
        stamina.Sub(amount);
        return true;
    }
}
