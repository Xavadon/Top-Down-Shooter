using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEnemyShooting : EnemyShooting
{
    [SerializeField] private float _shootCooldown = 1;
    [SerializeField] private float _bulletSpeed = 50f;
    [SerializeField] private Transform[] _bulletSpawnPoints;
    [SerializeField] private Color _bulletColor;

    private bool _isCooldown;
    private Pool _bulletPool;
    private AnimatorHandler _animatorHandler;
    private WaitForSeconds _shootCooldownCoroutine;

    private void OnValidate()
    {
        if (_shootCooldown <= 0) _shootCooldown = 1;
        if (_bulletSpeed <= 0) _bulletSpeed = 50f;
        if (_bulletColor.a == 0) _bulletColor.a = 255;
    }

    private void OnDisable()
    {
        StopCoroutine(SetShootCooldown());
        _isCooldown = false;
    }

    private void Start()
    {
        _animatorHandler = GetComponent<AnimatorHandler>();
        _bulletPool = BulletPoolSingleton.singleton.GetComponent<Pool>();
        _shootCooldownCoroutine = new WaitForSeconds(_shootCooldown);
    }

    public override void Shoot()
    {
        if (!_isCooldown && _bulletPool)
        {
            StartCoroutine(nameof(SetShootCooldown));
            _animatorHandler.PlayTargetAnimation("Shoot");
            for (int i = 0; i < _bulletSpawnPoints.Length; i++)
            {
                CreateBullet(i);
            }
        }
    }

    private IEnumerator SetShootCooldown()
    {
        _isCooldown = true;
        yield return _shootCooldownCoroutine;
        _isCooldown = false;
    }

    private PoolObject CreateBullet(int i)
    {
        var poolObject = _bulletPool.GetFreeElement(_bulletSpawnPoints[i].position);
        var bullet = poolObject.GetComponent<Bullet>();
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * _bulletSpeed;
        bullet.EnemyBullet = true;
        bullet.TrailRenderer.startColor = _bulletColor;
        bullet.TrailRenderer.endColor = _bulletColor;
        return poolObject;
    }
}
