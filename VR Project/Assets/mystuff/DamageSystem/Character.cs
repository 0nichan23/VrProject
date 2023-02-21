using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected Damageable damageable;
    [SerializeField] protected DamageDealer damageDealer;
    [SerializeField] protected DamageType targetType;
    public void SetTargetType(DamageType damageType)
    {
        targetType = damageType;
    }
    private void Awake()
    {
        SetUp();
    }
    protected virtual void SetUp()
    {
        SubscirbeElementalEvents();
    }
    protected void SubscirbeElementalEvents()
    {
        damageable.CacheTargetType(targetType);
        damageDealer.CacheTargetType(targetType);
    }
    public Damageable Damageable { get => damageable; }
    public DamageDealer DamageDealer { get => damageDealer; }
    public DamageType TargetType { get => targetType; }
}
