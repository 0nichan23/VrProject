using UnityEngine;
using UnityEngine.AI;

public class EnemyStater : MonoBehaviour
{
    [SerializeField] private Character target;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float strikingDistance;
    [SerializeField] private float strikingIntervals;
    [SerializeField] private float movementSpeed;
    [SerializeField] private Enemy refEnemy;
    private float lastAttacked;
    public Character Target { get => target; }

    private void Start()
    {
        lastAttacked = strikingIntervals * -1;
        SetDest();
    }
    public void SetTarget(Character givenTarget)
    {
        target = givenTarget;
        SetDest();
    }

    private void SetDest()
    {
        agent.destination = target.transform.position;
    }

    private void Update()
    {
        DetermineAction();
    }

    private void DetermineAction()
    {
        if (Vector3.Distance(target.transform.position, transform.position) <= strikingDistance)
        {
            agent.speed = 0f;
            Attack();
        }
        else
        {
            agent.speed = movementSpeed;
        }
    }

    protected virtual void Attack()
    {
        if (Time.time - lastAttacked < strikingIntervals)
        {
            return;
        }
        lastAttacked = Time.time;
        target.Damageable.GetHit(refEnemy.BasicAttack, refEnemy.DamageDealer);
        Debug.Log("Enemy is attacking");
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, strikingDistance);
    }

}
