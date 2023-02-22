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

    private void OnTriggerEnter(Collider other)
    {
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
