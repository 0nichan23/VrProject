using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class Damageable : MonoBehaviour
{
    [SerializeField] private float maxHp;
    private float currentHp;
    [SerializeField] private DamageType targetType;
    [SerializeField] private float invulnerabiltyDuration;
    private bool canTakeDamage;

    public UnityEvent<Attack> OnTakeDamge;
    public UnityEvent<Attack> OnTakeDamgeCalcDone;
    public UnityEvent<Attack> OnGetHit;
    public UnityEvent<DamageHandler> OnHeal;
    public UnityEvent OnDeath;

    public UnityEvent OnTakeDamageGFX;

    public DamageType TargetType { get => targetType; }

    public void GetHit(Attack givenAttack)
    {
        OnGetHit?.Invoke(givenAttack);
        TakeDamage(givenAttack);
    }

    public void GetHit(Attack givenAttack, DamageDealer givenDealer)
    {
        OnGetHit?.Invoke(givenAttack);
        givenDealer.OnHit?.Invoke(givenAttack);
        TakeDamage(givenAttack, givenDealer);
    }
    public void TakeDamage(Attack givenAttack)
    {
        if (!canTakeDamage)
        {
            return;
        }
        OnTakeDamge?.Invoke(givenAttack);
        OnTakeDamgeCalcDone?.Invoke(givenAttack);
        currentHp -= givenAttack.Damage.GetFinalDamage();
        StartCoroutine(InvulnerabilityTime());
        ClampHp();
        OnTakeDamageGFX?.Invoke();
        if (currentHp <= 0)
        {
            OnDeath?.Invoke();
        }
    }
    public void TakeDamage(Attack givenAttack, DamageDealer givenDealer)
    {
        if (!canTakeDamage)
        {
            return;
        }
        OnTakeDamge?.Invoke(givenAttack);
        givenDealer.OnDealDamage?.Invoke(givenAttack);
        OnTakeDamgeCalcDone?.Invoke(givenAttack);
        givenDealer.OnDamageCalcDone?.Invoke(givenAttack);
        currentHp -= givenAttack.Damage.GetFinalDamage();
        StartCoroutine(InvulnerabilityTime());
        ClampHp();
        OnTakeDamageGFX?.Invoke();
        if (currentHp <= 0)
        {
            OnDeath?.Invoke();
            givenDealer.OnKill?.Invoke(this);
        }
    }

    private void ClampHp()
    {
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);
    }

    private IEnumerator InvulnerabilityTime()
    {
        canTakeDamage = false;
        yield return new WaitForSecondsRealtime(invulnerabiltyDuration);
        canTakeDamage = false;

    }

}
