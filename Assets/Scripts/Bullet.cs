using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private short damage;
    
    private void OnCollisionEnter(Collision c)
    {
        var obj = c.gameObject.GetComponent<IDamageable>();
        obj?.OnDamage(damage);
        Destroy(this.gameObject);
    }
}
