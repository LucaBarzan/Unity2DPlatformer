using UnityEngine;

[CreateAssetMenu(fileName = "Damage", menuName = "Scriptable Objects/DamageSO")]
public class DamageSO : ScriptableObject
{
    public float Value;
    public bool CheckForObstacles;
}