using ShootEmUp;
using UnityEngine;

[RequireComponent(typeof(PlayerInput), typeof(PlayerMovement), typeof(PlayerAttacker))]

public class Player : Character
{
    private PlayerMovement _playerMovement;
    private PlayerAttacker _playerAttacker;
    private PlayerInput _playerInput;

    public PlayerMovement PlayerMovement => _playerMovement;
    public PlayerAttacker PlayerAttacker => _playerAttacker;
    public PlayerInput PlayerInput => _playerInput;

    public override void Initialize()
    {
        base.Initialize();

        _playerMovement = GetComponent<PlayerMovement>();
        _playerAttacker = GetComponent<PlayerAttacker>();
        _playerInput = GetComponent<PlayerInput>();

        _playerMovement.Initialize(_rigidbody2D, _characterCharacteristics.MoveSpeed);
    }
}