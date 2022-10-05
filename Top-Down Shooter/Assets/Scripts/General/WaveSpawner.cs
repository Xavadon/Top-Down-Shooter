using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private Vector3 _spawnRange;
    [SerializeField] private float _spawnDelay = 5f;
    [Space(height: 10)]
    [SerializeField] private Pool[] _enemyPool;
    [SerializeField] private int _enemyCount;
    [Space(height:10)]
    [SerializeField] private Pool _itemHealPool;
    [SerializeField] private int _healItemCountPerWave;
    private int _deadEnemyCount;

    private void Start()
    {
        Invoke(nameof(SpawnEnemies), _spawnDelay);
    }

    private void OnEnable()
    {
        UnitHealth.EnemyDied += CountDeadEnemy;
    }

    private void OnDisable()
    {
        UnitHealth.EnemyDied -= CountDeadEnemy;
    }

    private void CountDeadEnemy()
    {
        _deadEnemyCount++;
        if(_deadEnemyCount >= _enemyCount)
        {
            SpawnItemHeal();
            _deadEnemyCount = 0;
            _enemyCount += 2;
            Invoke(nameof(SpawnEnemies), _spawnDelay);
        }
    }

    private void SpawnEnemies()
    {
        if (_enemyPool[0])
        {
            for (int i = 0; i < _enemyCount; i++)
            {
                var position = new Vector3(Random.Range(-_spawnRange.x, _spawnRange.x), _spawnRange.y, Random.Range(-_spawnRange.z, _spawnRange.z));
                _enemyPool[Random.Range(0, _enemyPool.Length)].GetFreeElement(position);
            }
        }
    }

    private void SpawnItemHeal()
    {
        if (_itemHealPool)
        {
            for (int i = 0; i < _healItemCountPerWave; i++)
            {
                var position = new Vector3(Random.Range(-_spawnRange.x, _spawnRange.x), _spawnRange.y, Random.Range(-_spawnRange.z, _spawnRange.z));
                _itemHealPool.GetFreeElement(position);
            }
        }
    }
}
