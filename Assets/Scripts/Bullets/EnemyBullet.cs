using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private short damage;
    [SerializeField] private ParticleSystem hit;
    private MeshRenderer _mesh;
    private Rigidbody rb;

    private void Awake()
    {
        _mesh = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.CompareTag("Enemy")) return;
        rb.velocity=Vector3.zero;
        _mesh.enabled = false;
        hit.Play();
        var obj = c.gameObject.GetComponent<IDamageable>();
        obj?.OnDamage(damage);
        Destroy(this.gameObject,1.0f);
    }
    
}
