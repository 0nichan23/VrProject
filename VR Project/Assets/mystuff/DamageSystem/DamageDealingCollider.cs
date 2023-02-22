using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class DamageDealingCollider : MonoBehaviour
{
    [SerializeField] private Attack attack;
    [SerializeField] private DamageDealer dealer;
    public UnityEvent OnColliderHit;

    

    [SerializeField] private bool stayCollider;
    [Header("Stay Collider Only")]
    [SerializeField] private float strikingInervals;
    private float lastHit;
    public DamageDealer Dealer { get => dealer; }

    public void CacheDamageDealer(DamageDealer givenDealer)
    {
        dealer = givenDealer;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (stayCollider)
        {
            return;
        }
        Damageable damageableHit = other.gameObject.GetComponent<Damageable>();
        if (!ReferenceEquals(damageableHit, null))
        {
            if (ReferenceEquals(dealer, null))
            {
                damageableHit.GetHit(attack);
            }
            else
            {
                damageableHit.GetHit(attack, dealer);
            }
            OnColliderHit?.Invoke();
        }                   
    }


    private void OnTriggerStay(Collider other)
    {
        if (!stayCollider || Time.time - lastHit < strikingInervals)
        {
            return;
        }
        lastHit = Time.time;
        Damageable damageableHit = other.gameObject.GetComponent<Damageable>();
        if (!ReferenceEquals(damageableHit, null))
        {
            if (ReferenceEquals(dealer, null))
            {
                damageableHit.GetHit(attack);
            }
            else
            {
                damageableHit.GetHit(attack, dealer);
            }
            OnColliderHit?.Invoke();
        }
    }
}
