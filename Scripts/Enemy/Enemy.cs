using ShootEmUp;
using UnityEngine;

[RequireComponent (typeof(EnemyMovement), typeof(EnemyAttacker))]

public class Enemy : Character
{
    private EnemyMovement _enemyMovement;
    private EnemyAttacker _enemyAttacker;

    public EnemyMovement EnemyMovement => _enemyMovement;
    public EnemyAttacker EnemyAttacker => _enemyAttacker;

    public override void Initialize()
    {
        base.Initialize();

        _enemyMovement = GetComponent<EnemyMovement>();
        _enemyAttacker = GetComponent<EnemyAttacker>();

        _enemyMovement.Initialize(_rigidbody2D,_characterCharacteristics.MoveSpeed);
    }
}