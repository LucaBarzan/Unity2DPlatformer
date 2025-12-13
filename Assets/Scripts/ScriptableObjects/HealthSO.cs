using UnityEngine;

[CreateAssetMenu(fileName = "Health", menuName = "Scriptable Objects/Health")]
public class HealthSO : ScriptableObject
{
    [Header("Health")]
    public float MaxHealth = 100f;

    [Header("Damage Filters")]
    public DamageSO[] IgnoreDamageFrom;
    public DamageSO[] OnlyTakeDamageFrom;
}