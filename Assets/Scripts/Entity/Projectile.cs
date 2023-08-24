using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float projectileSpeed = 5f;
    [SerializeField]
    private float projectileDamage = 5f;
    [SerializeField]
    private Transform projectileOrigin = null;

    private float projectileDuration = 10f;
    private int bulletKey;
    private Rigidbody rb = null;
    public UnityEvent OnProjectileDestoryed = new UnityEvent();

    public void OnEnable()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        rb.AddForce(projectileOrigin.forward * projectileSpeed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == false && !other.CompareTag("Bullet"))
        {
            Health health = other.GetComponent<Health>();
            if(health != null)
            {
                health.UpdateHealth(projectileDamage);
            }
            DestroyProjectile();
        }
    }

    public void SetKey(int index)
    {
        bulletKey = index -1;
    }

    public int GetKey()
    {
        return bulletKey;
    }

    private void DestroyProjectile()
    {
        OnProjectileDestoryed?.Invoke();
        Destroy(gameObject);
    }
}
