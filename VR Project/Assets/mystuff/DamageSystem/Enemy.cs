using System;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private Attack basicAttack;

    public Attack BasicAttack { get => basicAttack; }

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
