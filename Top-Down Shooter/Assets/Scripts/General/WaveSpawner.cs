using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private Vector3 _spawnRange;
    [SerializeField] private float _spawnDelay = 5f;
    [SerializeField] private int _enemyCount;
    [SerializeField] private int _spawnHealItemCountPerWave = 2;
    [SerializeField] private Pool[] _enemyPool;
    [SerializeField] private Pool _itemHealPool;

    private int _deadEnemyCount;

    private void Start()
    {
        if (_enemyPool != null) Invoke(nameof(SpawnEnemies), _spawnDelay);
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
            _deadEnemyCount = 0;
            _enemyCount += 2;
            if (_enemyPool != null) Invoke(nameof(SpawnEnemies), _spawnDelay);
            if (_itemHealPool) SpawnItemHeal();
        }
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < _enemyCount; i++)
        {
            _enemyPool[Random.Range(0, _enemyPool.Length)].GetFreeElement(SetSpawnRange());
        }
    }

    private void SpawnItemHeal()
    {
        for (int i = 0; i < _spawnHealItemCountPerWave; i++)
        {
            _itemHealPool.GetFreeElement(SetSpawnRange());
        }
    }

    private Vector3 SetSpawnRange()
    {
        var position = new Vector3(Random.Range(-_spawnRange.x, _spawnRange.x), _spawnRange.y, Random.Range(-_spawnRange.z, _spawnRange.z));
        return position;
    }
}
