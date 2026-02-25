using ShootEmUp;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BulletCreator : MonoBehaviour
{
    private Bullet _prefab;
    private Transform _conteiner;
    private Transform _worldTransform;

    private readonly Queue<Bullet> _bulletPool = new();

    public event UnityAction<Bullet> CreatedBullet;

    public void Initialize(int initialCount, Bullet prefab, Transform conteiner, Transform worldTransform)
    {
        _prefab = prefab;
        _conteiner = conteiner;
        _worldTransform = worldTransform;

        CreatePoolBullets(initialCount);
    }

    public void CreatePoolBullets(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Bullet bullet = Instantiate(_prefab, _conteiner);
            _bulletPool.Enqueue(bullet);
        }
    }

    public void CreateBulletByArgs(Args args)
    {
        Bullet bullet = GetFreeBullet();

        bullet.SetValues(args.velocity, args.position, args.color, args.physicsLayer, args.damage, args.delay, args.isPlayer);

        CreatedBullet?.Invoke(bullet);
    }

    public void AddBulletPool(Bullet bullet)
    {
        bullet.transform.SetParent(_conteiner);
        _bulletPool.Enqueue(bullet);
    }

    private Bullet GetFreeBullet()
    {
        if (_bulletPool.TryDequeue(out var bullet))
            bullet.transform.SetParent(_worldTransform);
        else
            bullet = Instantiate(_prefab, _worldTransform);

        return bullet;
    }

    public struct Args
    {
        public Vector2 position;
        public Vector2 velocity;
        public Color color;
        public int physicsLayer;
        public int damage;
        public float delay;
        public bool isPlayer;
    }
}