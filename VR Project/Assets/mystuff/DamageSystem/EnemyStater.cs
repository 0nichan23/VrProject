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
    [SerializeField] private Animator anim;
    private float lastAttacked;
    public Character Target { get => target; }
    public float StrikingDistance { get => strikingDistance;}

    private void Start()
    {
        refEnemy.CacheStater(this);
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
        if (refEnemy.Damageable.CurrentHp <= 0)
        {
            Die();
        }
        if (Vector3.Distance(target.transform.position, transform.position) <= StrikingDistance)
        {
            anim.SetBool("Run", false);
            agent.speed = 0f;
            Attack();
        }
        else
        {
            agent.speed = movementSpeed;
            anim.SetBool("Run", true);
        }
    }

    protected virtual void Attack()
    {
        if (Time.time - lastAttacked < strikingIntervals)
        {
            return;
        }
        anim.SetTrigger("Attack");
        lastAttacked = Time.time;
    }

    protected virtual void Die()
    {
        anim.SetTrigger("Die");
        Debug.Log("Enemy is Dead");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, StrikingDistance);
    }

}
