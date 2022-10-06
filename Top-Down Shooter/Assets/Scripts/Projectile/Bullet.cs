using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PoolObject))]
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _bulletSpeed = 50;
    [SerializeField] private TrailRenderer _trailRenderer;

    private PoolObject _poolObject;

    public TrailRenderer TrailRenderer => _trailRenderer;
    public bool EnemyBullet;

    private void OnValidate()
    {
        if (_bulletSpeed < 0) _bulletSpeed = 0;
    }

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
        particle.startColor = TrailRenderer.startColor;
    }
}
