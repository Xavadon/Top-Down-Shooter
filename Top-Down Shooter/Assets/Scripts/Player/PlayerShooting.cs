using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimatorHandler))]
public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private float _shootingCooldown;
    private bool _isCooldown;
    [SerializeField] private Transform[] _bulletSpawnPoints;
    [SerializeField] private Color _bulletColor;
    [SerializeField] private RandomSoundPlayer _shootSoudPlayer;

    private AnimatorHandler _animatorHandler;
    [SerializeField] private Pool _bulletPool;

    private void Awake()
    {
        _animatorHandler = GetComponent<AnimatorHandler>();
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && _bulletPool)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (!_isCooldown)
        {
            SetShootCooldown();
            _animatorHandler.PlayTargetAnimation("Shoot");
            _shootSoudPlayer?.PlaySound();
            for (int i = 0; i < _bulletSpawnPoints.Length; i++)
            {
                CreateBullet(i).GetComponent<Rigidbody>().velocity = Quaternion.Euler(0, 90, 0) * transform.forward * 50;
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
        bullet.GetComponent<Bullet>().EnemyBullet = false;
        var trail = bullet.GetComponent<Bullet>().trailRenderer;
        trail.startColor = _bulletColor;
        trail.endColor = _bulletColor;
        return bullet;
    }

}
