using ShootEmUp;
using System.Collections;
using UnityEngine;

public class EnemyAttacker : CharacterAttacker
{
    private Transform _target;
    private Coroutine _coroutineAttack;

    public override void GenerateBulletArgs()
    {
        Vector2 direction = (Vector2)(_target.transform.position - _firePoint.position).normalized;

        _argsBullet = new BulletCreator.Args
        {
            isPlayer = false,
            physicsLayer = (int)_bulletConfig.physicsLayer,
            color = _bulletConfig.color,
            damage = _bulletConfig.damage,
            position = _firePoint.position,
            velocity = direction * _bulletConfig.speed
        };
    }

    public override bool TryShoot()
    {
        if (_target == null)
            _canShoot = false;

        return base.TryShoot();
    }

    public void ActivateAttack(Transform target)
    {
        _target = target;
        _coroutineAttack = StartCoroutine(UseAttack());
    }

    public void DeactivateAttack() => StopCoroutine(UseAttack());

    public IEnumerator UseAttack()
    {
        while (true)
        {
            if (TryShoot())
                Shoot();

            yield return null;
        }
    }
}