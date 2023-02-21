using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageDealer : MonoBehaviour
{
    public UnityEvent<Attack> OnDealDamage;
    public UnityEvent<Attack> OnHit;
    public UnityEvent<Attack> OnDamageCalcDone;
    public UnityEvent<Damageable> OnKill;
    private DamageType targetType;
    public void CacheTargetType(DamageType targetType)
    {
        OnDamageCalcDone.RemoveListener(ElementalDamageBoost);
        this.targetType = targetType;
        OnDamageCalcDone.AddListener(ElementalDamageBoost);
    }

    private void ElementalDamageBoost(Attack attack)
    {
        if (attack.DamageType == targetType)//if youre using the same element as your own type add damage
        {
            attack.Damage.AddMod(1.5f);
        }
    }
}
