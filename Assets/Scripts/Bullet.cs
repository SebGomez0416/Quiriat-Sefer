using System;
using UnityEngine;

public class Bullet : MonoBehaviour
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
        rb.velocity=Vector3.zero;
        _mesh.enabled = false;
        hit.Play();
        var obj = c.gameObject.GetComponent<IDamageable>();
        obj?.OnDamage(damage);
        Destroy(this.gameObject,1.0f);
    }
}
