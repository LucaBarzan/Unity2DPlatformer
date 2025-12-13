using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Events")]
    public UnityEngine.Events.UnityEvent OnHealed;
    public UnityEngine.Events.UnityEvent<AttackInfo> OnTakeDamage;
    public UnityEngine.Events.UnityEvent<AttackInfo> OnHealthEmpty;

    public float CurrentHealth => currentHealth;
    public float MaxHealth => health.MaxHealth;
    public float HealthPercentage => currentHealth / MaxHealth;

    [SerializeField] private HealthSO health;

    private float currentHealth;
    private HashSet<DamageSO> ignoreDamageFrom;
    private HashSet<DamageSO> onlyTakeDamageFrom;

    private void Awake()
    {
        SetHealth(MaxHealth);
        ignoreDamageFrom = new HashSet<DamageSO>(health.IgnoreDamageFrom);
        onlyTakeDamageFrom = new HashSet<DamageSO>(health.OnlyTakeDamageFrom);
    }
    
    #region Damage / Healing

    public bool TakeDamage(DamageSO damage) => TakeDamage(new AttackInfo(damage));

    public bool TakeDamage(AttackInfo attackInfo)
    {
        if (!enabled || attackInfo.Damage == null)
            return false;

        if (!CanTakeDamageFrom(attackInfo.Damage))
            return false;

        // Try Apply damage
        return SetHealth(currentHealth - attackInfo.Damage.Value, attackInfo);
    }

    public bool Heal(float healAmount)
    {
        if(healAmount <= 0f)
            return false;

        return SetHealth(currentHealth + healAmount);
    }

    #endregion Damage / Healing

    public bool SetHealth(float newHealth, AttackInfo attackInfo = default)
    {
        if (!TryUpdateHealth(newHealth, out var increased))
            return false;

        RaiseHealthEvents(increased, attackInfo);
        return true;
    }

    private bool TryUpdateHealth(float newHealth, out bool increased)
    {
        newHealth = Mathf.Clamp(newHealth, 0f, MaxHealth);

        increased = newHealth > currentHealth;

        if (Mathf.Approximately(newHealth, currentHealth))
            return false;

        currentHealth = newHealth;
        return true;
    }

    private bool CanTakeDamageFrom(DamageSO damage)
    {
        if (ignoreDamageFrom != null && 
            ignoreDamageFrom.Count > 0 && 
            ignoreDamageFrom.Contains(damage))
            return false;

        if (onlyTakeDamageFrom != null && 
            onlyTakeDamageFrom.Count > 0 && 
            !onlyTakeDamageFrom.Contains(damage))
            return false;

        return true;
    }

    private void RaiseHealthEvents(bool increased, AttackInfo damageInfo)
    {
        if (currentHealth <= 0.0f)
        {
            OnHealthEmpty?.Invoke(damageInfo);
            return;
        }

        if (increased)
        {
            OnHealed?.Invoke();
            return;
        }

        OnTakeDamage?.Invoke(damageInfo);
    }
}