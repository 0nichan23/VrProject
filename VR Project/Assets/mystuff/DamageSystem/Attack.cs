using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Attack")]
public class Attack : ScriptableObject
{
    [SerializeField] private DamageHandler damage = new DamageHandler();
    [SerializeField] private DamageType damageType;

    public DamageHandler Damage { get => damage; }
    public DamageType DamageType { get => damageType; }
}

public enum DamageType
{
    Ice,
    Fire,
    Lightning
}