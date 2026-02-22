using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(BulletCreator), typeof(BulletFilter))]

    public sealed class BulletSystem : MonoBehaviour, IGameFixedUpdateListener, IGamePlayListener, IGamePauseListener, IGameResumeListener, IGameFinishListener
    {
        [SerializeField] private int _initialCount = 50;
        [Space]
        [SerializeField] private Transform _container;
        [SerializeField] private Bullet _prefab;
        [SerializeField] private Transform _worldTransform;
        [SerializeField] private LevelBounds _levelBounds;

        private BulletFilter _bulletFilter;
        private BulletCreator _bulletCreator;

        private readonly HashSet<Bullet> _activeBullets = new();

        public void Initialize()
        {
            _bulletFilter = GetComponent<BulletFilter>();
            _bulletCreator = GetComponent<BulletCreator>();

            _bulletFilter.Initialize(_levelBounds);
            _bulletCreator.Initialize(_initialCount, _prefab, _container, _worldTransform);
        }

        public void FixedUpdateGame()
        {
            _bulletFilter.UseFilterBullets(_activeBullets);
        }

        public void PlayGame()
        {
            _bulletCreator.CreatedBullet += OnCreatedBullet;
            _bulletFilter.BulletLeavedBounds += OnBulletLeavedBounds;
        }

        public void PauseGame()
        {
            foreach (var activeBullet in _activeBullets) { activeBullet.Activate(false); }
        }

        public void ResumeGame()
        {
            foreach (var activeBullet in _activeBullets) { activeBullet.Activate(true); }
        }

        public void FinishGame()
        {
            _bulletCreator.CreatedBullet -= OnCreatedBullet;
            _bulletFilter.BulletLeavedBounds -= OnBulletLeavedBounds;
        }

        public void FlyBulletByArgs(BulletCreator.Args args)
        {
            _bulletCreator.CreateBulletByArgs(args);
        }

        private void OnCreatedBullet(Bullet bullet)
        {
            if (_activeBullets.Add(bullet))
                bullet.OnCollisionEntered += OnBulletCollision;
        }

        private void OnBulletLeavedBounds(Bullet bullet) => RemoveBullet(bullet);

        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out IHealth healthCharacter))
                healthCharacter.TakeDamage(bullet.Damage);

            RemoveBullet(bullet);
        }

        private void RemoveBullet(Bullet bullet)
        {
            if (_activeBullets.Remove(bullet))
            {
                bullet.OnCollisionEntered -= OnBulletCollision;
                _bulletCreator.AddBulletPool(bullet);
            }
        }
    }
}