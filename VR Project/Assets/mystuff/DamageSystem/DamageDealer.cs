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

}
