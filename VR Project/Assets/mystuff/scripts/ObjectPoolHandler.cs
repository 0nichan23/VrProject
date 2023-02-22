using UnityEngine;

public class ObjectPoolHandler : MonoBehaviour
{
    [SerializeField] private EnemyOP enemyOP;
    [SerializeField] private BulletOP fireProjectileOP;
    [SerializeField] private BulletOP iceProjectileOP;
    [SerializeField] private ExplosionOp explosionOP;
    public EnemyOP EnemyOP { get => enemyOP; }
    public BulletOP FireProjectileOP { get => fireProjectileOP; }
    public BulletOP IceProjectileOP { get => iceProjectileOP; }
    public ExplosionOp ExplosionOP { get => explosionOP; }

    private void Start()
    {
        GameManager.instance.ObjectPoolHandler = this;
    }
}
