using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PoolObject))]
public class Bullet : MonoBehaviour
{
    public TrailRenderer trailRenderer;

    [SerializeField] private float _damage;
    private PoolObject _poolObject;

    public bool EnemyBullet;

    private void Start()
    {
        _poolObject = GetComponent<PoolObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            if (EnemyBullet && other.TryGetComponent(out Enemy enemy)) return;
            if (!EnemyBullet && other.TryGetComponent(out Player player)) return;

            damageable.ApplyDamage(_damage);
            DestroyBullet();
        }
        if (!other.TryGetComponent<PoolObject>(out PoolObject poolObject))
        {
            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        CreateExplotion();
        _poolObject.ReturnToPool();
    }

    private void CreateExplotion()
    {
        var explotion = ExplotionPoolSingleton.singleton.GetComponent<Pool>().GetFreeElement(transform.position);
        var particle = explotion.GetComponent<ParticleSystem>().main;
        particle.startColor = trailRenderer.startColor;
    }
}
