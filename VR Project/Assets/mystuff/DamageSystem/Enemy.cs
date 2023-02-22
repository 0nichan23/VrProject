using System;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private Attack basicAttack;
    private EnemyStater refStater;
    public Attack BasicAttack { get => basicAttack; }
    public EnemyStater RefStater { get => refStater; }

    public void CacheStater(EnemyStater givenStater)
    {
        refStater = givenStater;
    }



    protected override void SetUp()
    {
        base.SetUp();
        RollRandomTargetType();
        SubscirbeElementalEvents();
    }

    private void RollRandomTargetType()
    {
        targetType = (DamageType)UnityEngine.Random.Range(0, Enum.GetValues(typeof(DamageType)).Length);
    }
}
