using ShootEmUp;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Player _player;

    private BulletSystem _bulletSystem;

    public Player Player => _player;

    public event UnityAction DeathedPlayer;

    private void OnDisable() => UnSubscribeEvents();

    public void Initialize(BulletSystem bulletSystem)
    {
        _bulletSystem = bulletSystem;
        _player.Initialize();

        SubscribeEvents();
    }

    public void UseActions()
    {
        _player.PlayerInput.TryInputKeyCodes();
    }

    public void UseActionsFixedTime()
    {
        _player.PlayerMovement.Move();
    }

    private void SubscribeEvents()
    {
        _player.PlayerInput.PressedKeyMove += OnPressedKeyMove;
        _player.PlayerInput.PressedKeyShoot += OnPressedKeyShoot;
        _player.PlayerAttacker.UsedShoot += OnUsedShoot;
        _player.Deathed += OnDeathed;
    }

    private void UnSubscribeEvents()
    {
        _player.PlayerInput.PressedKeyMove -= OnPressedKeyMove;
        _player.PlayerInput.PressedKeyShoot -= OnPressedKeyShoot;
        _player.PlayerAttacker.UsedShoot -= OnUsedShoot;
        _player.Deathed -= OnDeathed;
    }

    private void OnPressedKeyMove(float direction) => _player.PlayerMovement.SetDirectionMove(direction);

    private void OnPressedKeyShoot()
    {
        if (_player.PlayerAttacker.TryShoot())
            _player.PlayerAttacker.Shoot();
    }

    private void OnUsedShoot(BulletCreator.Args bulletArgs) => _bulletSystem.FlyBulletByArgs(bulletArgs);

    private void OnDeathed(Character character) => DeathedPlayer?.Invoke();
}