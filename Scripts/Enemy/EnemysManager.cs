using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(EnemySpawner))]

    public sealed class EnemysManager : MonoBehaviour, IGamePlayListener, IGameFinishListener, IGamePauseListener, IGameResumeListener
    {
        private EnemySpawner _enemySpawner;
        private BulletSystem _bulletSystem;
        private Transform _target;

        private readonly List<Enemy> _activeEnemies = new();

        public void Initialize(BulletSystem bulletSystem, Transform target)
        {
            _enemySpawner = GetComponent<EnemySpawner>();

            _bulletSystem = bulletSystem;
            _target = target;
        }

        public void PlayGame()
        {
            _enemySpawner.EnemySpawned += OnEnemySpawned;

            _enemySpawner.ActivateSpawnEnemys();
        }

        public void FinishGame()
        {
            _enemySpawner.EnemySpawned -= OnEnemySpawned;

            foreach (var activeEnemy in _activeEnemies)
            {
                activeEnemy.EnemyAttacker.UsedShoot -= OnUsedShoot;
                activeEnemy.EnemyMovement.ReachedPlace -= OnReachedPlace;
            }
        }

        public void PauseGame()
        {
            _enemySpawner.DeactivateSpawnEnemys();

            foreach (var activeEnemy in _activeEnemies)
            {
                activeEnemy.EnemyAttacker.DeactivateAttack();
                activeEnemy.EnemyMovement.DeactivateMove();
            }
        }

        public void ResumeGame()
        {
            _enemySpawner.ActivateSpawnEnemys();

            foreach (var activeEnemy in _activeEnemies)
            {
                activeEnemy.EnemyAttacker.ActivateAttack();
                activeEnemy.EnemyMovement.ActivateMove();
            }
        }

        private void OnEnemySpawned(Enemy enemy)
        {
            _activeEnemies.Add(enemy);
            enemy.EnemyMovement.ActivateMove();
            enemy.EnemyAttacker.SetTarget(_target);

            enemy.EnemyAttacker.UsedShoot += OnUsedShoot;
            enemy.EnemyMovement.ReachedPlace += OnReachedPlace;
            enemy.Deathed += OnDeathed;
        }

        private void OnReachedPlace(Enemy enemy)
        {
            if (enemy != null)
                enemy.EnemyAttacker.ActivateAttack();
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