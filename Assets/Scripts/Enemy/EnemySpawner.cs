using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn")]
    [SerializeField] private int _maxCountEnemys;
    [SerializeField] private int _delaySpawnEnemy;
    [SerializeField] private Transform _worldTransform;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private List<Transform> _attackPositions;
    [Header("Pool")]
    [SerializeField] private Transform _container;
    [SerializeField] private Enemy _prefab;

    private Coroutine _creatorEnemys;
    private List<Transform> _openAttackPositions = new();
    private readonly Queue<Enemy> _enemyPool = new();

    public event UnityAction<Enemy> EnemySpawned;

    public void ActivateSpawnEnemys()
    {
        FillPoolEnemys();
        _creatorEnemys = StartCoroutine(CreateEnemys());
        _openAttackPositions = new List<Transform>(_attackPositions);
    }

    public void DeactivateSpawnEnemys() => StopCoroutine(_creatorEnemys);

    public void UnspawnEnemy(Enemy enemy)
    {
        enemy.transform.SetParent(_container);
        _openAttackPositions.Add(enemy.EnemyMovement.AttackPoint);
        _enemyPool.Enqueue(enemy);
    }

    private void FillPoolEnemys()
    {
        for (var i = 0; i < _maxCountEnemys; i++)
        {
            Enemy enemy = Instantiate(_prefab, _container);
            enemy.Initialize();
            _enemyPool.Enqueue(enemy);
        }
    }

    private IEnumerator CreateEnemys()
    {
        while (true)
        {
            yield return new WaitForSeconds(_delaySpawnEnemy);

            Enemy enemy = TryGetFreeEnemy();

            if (enemy != null && _openAttackPositions.Count > 0)
                SpawnEnemy(enemy);
        }
    }

    private Enemy TryGetFreeEnemy()
    {
        if (!_enemyPool.TryDequeue(out Enemy enemy))
            return null;

        return enemy;
    }

    private void SpawnEnemy(Enemy enemy)
    {
        Transform spawnPosition = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
        Transform attackPosition = _attackPositions[Random.Range(0, _attackPositions.Count)];

        _openAttackPositions.Remove(attackPosition);
        enemy.transform.SetParent(_worldTransform);
        enemy.transform.position = spawnPosition.position;
        enemy.EnemyMovement.SetDestination(attackPosition);

        EnemySpawned?.Invoke(enemy);
    }
}