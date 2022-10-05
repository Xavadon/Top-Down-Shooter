using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEnemyShooting : EnemyShooting
{
    [Space(height: 10)]
    [SerializeField] private float _shootingCooldown = 1;
    private bool _isCooldown;
    [SerializeField] private Transform[] _bulletSpawnPoints;
    [SerializeField] private Color _bulletColor;

    private Pool _bulletPool;
    private AnimatorHandler _animatorHandler;


    private void Start()
    {
        _animatorHandler = GetComponent<AnimatorHandler>();
        _bulletPool = BulletPoolSingleton.singleton.GetComponent<Pool>();
    }

    public override void Shoot()
    {
        if (!_isCooldown && _bulletPool)
        {
            SetShootCooldown();
            _animatorHandler.PlayTargetAnimation("Shoot");
            for (int i = 0; i < _bulletSpawnPoints.Length; i++)
            {
                CreateBullet(i).GetComponent<Rigidbody>().velocity = transform.forward * 50;
            }
        }
    }

    private void SetShootCooldown()
    {
        _isCooldown = true;
        Invoke(nameof(ResetCooldown), _shootingCooldown);
    }

    private void ResetCooldown()
    {
        _isCooldown = false;
    }

    private PoolObject CreateBullet(int i)
    {
        var bullet = _bulletPool.GetFreeElement(_bulletSpawnPoints[i].position);
        bullet.GetComponent<Bullet>().EnemyBullet = true;
        var trail = bullet.GetComponent<Bullet>().trailRenderer;
        trail.startColor = _bulletColor;
        trail.endColor = _bulletColor;
        return bullet;
    }
}
