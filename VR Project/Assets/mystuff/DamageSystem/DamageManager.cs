using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{

    public void SetupEvents(Damageable givenDamageable)
    {
        switch (givenDamageable.TargetType)
        {
            case DamageType.Ice:
                givenDamageable.OnTakeDamge.AddListener(IceEvents);
                break;
            case DamageType.Fire:
                givenDamageable.OnTakeDamge.AddListener(FireEvents);
                break;
            case DamageType.Lightning:
                givenDamageable.OnTakeDamge.AddListener(LightningEvents);
                break;
        }
    }

    private void IceEvents(Attack givenAttack)
    {
        if (givenAttack.DamageType == DamageType.Fire)
        {
            givenAttack.Damage.AddMod(1.5f);
        }
        else if (givenAttack.DamageType == DamageType.Ice)
        {
            givenAttack.Damage.AddMod(0f);

        }
    }
    private void FireEvents(Attack givenAttack)
    {
        if (givenAttack.DamageType == DamageType.Lightning)
        {
            givenAttack.Damage.AddMod(1.5f);
        }
        else if (givenAttack.DamageType == DamageType.Fire)
        {
            givenAttack.Damage.AddMod(0f);

        }
    }
    private void LightningEvents(Attack givenAttack)
    {
        if (givenAttack.DamageType == DamageType.Ice)
        {
            givenAttack.Damage.AddMod(1.5f);
        }
        else if (givenAttack.DamageType == DamageType.Lightning)
        {
            givenAttack.Damage.AddMod(0f);

        }
    }

}
