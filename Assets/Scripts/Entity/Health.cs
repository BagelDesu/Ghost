using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 0f;

    private float curHealth = 0f;

    private UnityEvent<float> OnHealthChanged = new UnityEvent<float>();
    private UnityEvent OnEntityDeath;

    private void OnEnable()
    {
        curHealth = maxHealth;
    }

    /// <summary>
    /// Used to affect the health of the entity.
    /// </summary>
    /// <param name="amount">The amount to be added to the Entity, If the purpose of the change is to damage the entity add a negative value</param>
    public void UpdateHealth(float amount)
    {
        curHealth += amount;
        // If the health is less than zero, or greater than the maximum allowed. Short the conditionals, so we don't update the event.
        if(curHealth <= 0f)
        {
            curHealth = 0f;
            KillEntity();
            return;
        }
        else if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
            return;
        }

        OnHealthChanged?.Invoke(amount);
    }

    private void KillEntity()
    {
        OnEntityDeath?.Invoke();
        OnEntityDeath?.RemoveAllListeners();
        Destroy(this.gameObject);
    }
}
