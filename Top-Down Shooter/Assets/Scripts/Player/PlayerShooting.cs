using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimatorHandler))]
public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private float _shootingCooldown = 0.15f;
    [SerializeField] private float _bulletSpeed = 50f;
    [SerializeField] private Transform[] _bulletSpawnPoints;
    [SerializeField] private Color _bulletColor;
    [SerializeField] private RandomSoundPlayer _shootSoudPlayer;
    [SerializeField] private Pool _bulletPool;

    private bool _isCooldown; 
    private AnimatorHandler _animatorHandler;

    private void OnValidate()
    {
        if (_shootingCooldown <= 0) _shootingCooldown = 0.15f;
        if (_bulletSpeed <= 0) _bulletSpeed = 50f;
        if (_bulletColor.a == 0) _bulletColor.a = 255;
    }

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
                CreateBullet(i);
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
        var poolObject = _bulletPool.GetFreeElement(_bulletSpawnPoints[i].position);
        var bullet = poolObject.GetComponent<Bullet>();
        bullet.GetComponent<Rigidbody>().velocity = Quaternion.Euler(0, 90, 0) * transform.forward * _bulletSpeed;
        bullet.EnemyBullet = false;
        bullet.TrailRenderer.startColor = _bulletColor;
        bullet.TrailRenderer.endColor = _bulletColor;
        return poolObject;
    }

}
