using ShootEmUp;
using UnityEngine;
using UnityEngine.Events;

public abstract class CharacterAttacker : MonoBehaviour, IAttacker
{
    [SerializeField] protected Transform _firePoint;
    [SerializeField] protected BulletConfig _bulletConfig;

    protected bool _canShoot = true;
    protected BulletCreator.Args _argsBullet;

    public bool CanShoot => _canShoot;
    public float DelayShoot => _bulletConfig.delay;

    public event UnityAction<BulletCreator.Args> UsedShoot;

    public abstract void GenerateBulletArgs();

    public virtual bool TryShoot()
    {
        return _canShoot;
    }

    public virtual void Shoot()
    {
        _canShoot = false;

        GenerateBulletArgs();

        UsedShoot?.Invoke(_argsBullet);

        Invoke(nameof(Reload), DelayShoot);
    }

    public virtual void Reload() => _canShoot = true;
}
