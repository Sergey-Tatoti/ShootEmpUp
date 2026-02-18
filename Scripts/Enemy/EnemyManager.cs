using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(EnemySpawner))]

    public sealed class EnemyManager : MonoBehaviour
    {
        private EnemySpawner _enemySpawner;
        private BulletSystem _bulletSystem;
        private Transform _target;

        private readonly List<Enemy> _activeEnemies = new();

        private void OnDisable() => UnSubscribeEvents();

        public void Initialize(BulletSystem bulletSystem, Transform target)
        {
            _enemySpawner = GetComponent<EnemySpawner>();

            _bulletSystem = bulletSystem;
            _target = target;

            SubscribeEvents();
        }

        public void ActivateEnemys() => _enemySpawner.ActivateSpawnEnemys();

        public void DeactivateEnemys()
        {
            _enemySpawner.DeactivateSpawnEnemys();

            for (int i = 0; i < _activeEnemies.Count; i++)
            {
                _activeEnemies[i].EnemyAttacker.DeactivateAttack();
            }
        }

        private void SubscribeEvents()
        {
            _enemySpawner.EnemySpawned += OnEnemySpawned;
        }

        private void UnSubscribeEvents()
        {
            _enemySpawner.EnemySpawned -= OnEnemySpawned;
        }

        private void OnEnemySpawned(Enemy enemy)
        {
            _activeEnemies.Add(enemy);
            enemy.EnemyMovement.ActivateMove();

            enemy.EnemyAttacker.UsedShoot += OnUsedShoot;
            enemy.EnemyMovement.ReachedPlace += OnReachedPlace;
            enemy.Deathed += OnDeathed;
        }

        private void OnReachedPlace(Enemy enemy)
        {
            if (enemy != null)
                enemy.EnemyAttacker.ActivateAttack(_target);
        }

        private void OnDeathed(Character character)
        {
            Enemy enemy = character.GetComponent<Enemy>();
            enemy.EnemyMovement.DeactivateMove();

            if (_activeEnemies.Remove(enemy))
            {
                enemy.EnemyAttacker.UsedShoot -= OnUsedShoot;
                enemy.EnemyMovement.ReachedPlace -= OnReachedPlace;
                enemy.Deathed -= OnDeathed;

                _enemySpawner.UnspawnEnemy(enemy);
            }
        }

        private void OnUsedShoot(BulletCreator.Args bulletArgs) => _bulletSystem.FlyBulletByArgs(bulletArgs);
    }
}