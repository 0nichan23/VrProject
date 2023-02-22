using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimationEvents : MonoBehaviour
{
    [SerializeField] private Enemy refEnemy;

    public void DealDamageToTarget()
    {
        if (Vector3.Distance(refEnemy.RefStater.Target.transform.position, transform.position) <= refEnemy.RefStater.StrikingDistance)
            refEnemy.RefStater.Target.Damageable.GetHit(refEnemy.BasicAttack, refEnemy.DamageDealer);
    }

    public void Die()
    {
        refEnemy.gameObject.SetActive(false);
    }


}
