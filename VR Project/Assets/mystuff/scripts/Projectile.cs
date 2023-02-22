using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private DamageDealingCollider d_collider;
    [SerializeField] private bool explosive;
    [SerializeField] private GameObject explosion;
    [SerializeField] private float lifeTime;
    private bool exploded;

    public DamageDealingCollider D_collider { get => d_collider; }
    public float Speed { get => _speed; }
    public GameObject Explosion { get => explosion; }
    private void OnEnable()
    {
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
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    private void Explode()
    {
        if (!explosive || exploded)
        {
            return;
        }
        exploded = true;
        explosion.SetActive(true);
        //explode
    }

 
}
