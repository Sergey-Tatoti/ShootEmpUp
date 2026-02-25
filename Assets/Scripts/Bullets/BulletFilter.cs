using ShootEmUp;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BulletFilter : MonoBehaviour
{
    private LevelBounds _levelBounds;

    private readonly List<Bullet> _bulletCache = new();

    public event UnityAction<Bullet> BulletLeavedBounds;

    public void Initialize(LevelBounds levelBounds)
    {
        _levelBounds = levelBounds;
    }

    public void UseFilterBullets(HashSet<Bullet> _activeBullets)
    {
        _bulletCache.Clear();
        _bulletCache.AddRange(_activeBullets);

        for (int i = 0, count = _bulletCache.Count; i < count; i++)
        {
            Bullet bullet = _bulletCache[i];
            TryBulletLeavedBounds(bullet);
        }
    }

    private void TryBulletLeavedBounds(Bullet bullet)
    {
        if (!_levelBounds.InBounds(bullet.transform.position))
            BulletLeavedBounds?.Invoke(bullet);
    }
}