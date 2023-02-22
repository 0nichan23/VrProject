using UnityEngine;

public class ObjectPoolHandler : MonoBehaviour
{
    [SerializeField] private EnemyOP enemyOP;
    [SerializeField] private BulletOP fireProjectileOP;
    [SerializeField] private BulletOP iceProjectileOP;
    public EnemyOP EnemyOP { get => enemyOP; }
    public BulletOP FireProjectileOP { get => fireProjectileOP; }
    public BulletOP IceProjectileOP { get => iceProjectileOP; }

    private void Start()
    {
        GameManager.instance.ObjectPoolHandler = this;
    }
}
