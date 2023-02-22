using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private DamageDealingCollider d_collider;
    [SerializeField] private bool explosive;
    [SerializeField] private GameObject explosion;
    [SerializeField] private float lifeTime;
    private float currentSpeed;
    private bool exploded;

    public DamageDealingCollider D_collider { get => d_collider; }
    public float Speed { get => _speed; }
    public GameObject Explosion { get => explosion; }
    private void OnEnable()
    {
        currentSpeed = _speed;
        exploded = false;
        Invoke("Explode", lifeTime);
    }
    private void Awake()
    {
        D_collider.OnColliderHit.AddListener(Explode);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * currentSpeed * Time.deltaTime;
    }

    private void Explode()
    {
        if (!explosive || exploded)
        {
            return;
        }
        currentSpeed = 0f;
        exploded = true;
        DamageDealingCollider explosion = GameManager.instance.ObjectPoolHandler.ExplosionOP.GetPooledObject();
        explosion.CacheDamageDealer(d_collider.Dealer);
        explosion.transform.position = transform.position;
        explosion.gameObject.SetActive(true);
        gameObject.SetActive(false);
        //explode
    }

 
}
