using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [SerializeField] private float maxHp;
    private float currentHp;
    private DamageType targetType;
    [SerializeField] private float invulnerabiltyDuration;
    private bool canTakeDamage = true;

    public UnityEvent<Attack> OnTakeDamge;
    public UnityEvent<Attack> OnTakeDamgeCalcDone;
    public UnityEvent<Attack> OnGetHit;
    public UnityEvent<DamageHandler> OnHeal;
    public UnityEvent OnDeath;

    public UnityEvent OnTakeDamageGFX;

    public DamageType TargetType { get => targetType; }
    public float CurrentHp { get => currentHp; }
    public float MaxHp { get => maxHp; }

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
        givenAttack.Damage.ClearMods();
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
        givenAttack.Damage.ClearMods();
        StartCoroutine(InvulnerabilityTime());
        ClampHp();
        Debug.Log(name + " taking damage");
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
        canTakeDamage = true;

    }

    public void CacheTargetType(DamageType targetType)
    {
        currentHp = maxHp;
        OnTakeDamgeCalcDone.RemoveListener(ElementalDamageReduction);
        this.targetType = targetType;
        OnTakeDamgeCalcDone.AddListener(ElementalDamageReduction);
    }

    private void ElementalDamageReduction(Attack givenAttack)
    {
        switch (givenAttack.DamageType)
        {
            case DamageType.Ice:
                if (givenAttack.DamageType == DamageType.Lightning)
                {
                    givenAttack.Damage.AddMod(1.5f);
                }
                else if (givenAttack.DamageType == DamageType.Fire)
                {
                    givenAttack.Damage.AddMod(0.5f);
                }
                return;
            case DamageType.Fire:
                if (givenAttack.DamageType == DamageType.Ice)
                {
                    givenAttack.Damage.AddMod(1.5f);
                }
                else if (givenAttack.DamageType == DamageType.Lightning)
                {
                    givenAttack.Damage.AddMod(0.5f);
                }
                return;
            case DamageType.Lightning:
                if (givenAttack.DamageType == DamageType.Fire)
                {
                    givenAttack.Damage.AddMod(1.5f);
                }
                else if (givenAttack.DamageType == DamageType.Ice)
                {
                    givenAttack.Damage.AddMod(0.5f);
                }
                return;
        }
    }

}
