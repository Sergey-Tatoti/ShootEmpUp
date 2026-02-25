using UnityEngine.Events;

public interface IAttacker
{
    bool CanShoot { get; }
    float DelayShoot { get; }

    bool TryShoot();

    void GenerateBulletArgs();

    void Shoot();

    void Reload();

    event UnityAction<BulletCreator.Args> UsedShoot;
}