using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(BulletCreator), typeof(BulletFilter))]

    public sealed class BulletSystem : MonoBehaviour
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

        private void OnDisable() => UnSubscribeEvents();

        public void Initialize()
        {
            _bulletFilter = GetComponent<BulletFilter>();
            _bulletCreator = GetComponent<BulletCreator>();

            _bulletFilter.Initialize(_levelBounds);
            _bulletCreator.Initialize(_initialCount, _prefab, _container, _worldTransform);

            SubscribeEvents();
        }
        
        public void UseActionsFixedTime()
        {
            _bulletFilter.UseFilterBullets(_activeBullets);
        }

        public void FlyBulletByArgs(BulletCreator.Args args)
        {
            _bulletCreator.CreateBulletByArgs(args);
        }

        private void SubscribeEvents()
        {
            _bulletCreator.CreatedBullet += OnCreatedBullet;
            _bulletFilter.BulletLeavedBounds += OnBulletLeavedBounds;
        }

        private void UnSubscribeEvents()
        {
            _bulletCreator.CreatedBullet -= OnCreatedBullet;
            _bulletFilter.BulletLeavedBounds -= OnBulletLeavedBounds;
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