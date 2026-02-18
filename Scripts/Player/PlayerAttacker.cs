using UnityEngine;

public class PlayerAttacker : CharacterAttacker
{
    public override void GenerateBulletArgs()
    {
        _argsBullet = new BulletCreator.Args
        {
            isPlayer = true,
            physicsLayer = (int)_bulletConfig.physicsLayer,
            color = _bulletConfig.color,
            damage = _bulletConfig.damage,
            delay = _bulletConfig.delay,
            position = _firePoint.position,
            velocity = _firePoint.rotation * Vector3.up * _bulletConfig.speed
        };
    }

    public override void Shoot()
    {
        base.Shoot();
    }
}