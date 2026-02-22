using ShootEmUp;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour, IGameUpdateListener, IGameFixedUpdateListener, IGamePlayListener, IGamePauseListener, IGameFinishListener
{
    [SerializeField] private Player _player;

    private BulletSystem _bulletSystem;

    public Player Player => _player;

    public event UnityAction DeathedPlayer;

    public void Initialize(BulletSystem bulletSystem)
    {
        _bulletSystem = bulletSystem;
        _player.Initialize();
    }

    public void UpdateGame() => _player.PlayerInput.TryInputKeyCodes();

    public void FixedUpdateGame() => _player.PlayerMovement.Move();

    public void PauseGame() => _player.PlayerMovement.SetDirectionMove(0);

    public void PlayGame()
    {
        _player.PlayerInput.PressedKeyMove += OnPressedKeyMove;
        _player.PlayerInput.PressedKeyShoot += OnPressedKeyShoot;
        _player.PlayerAttacker.UsedShoot += OnUsedShoot;
        _player.Deathed += OnDeathed;
    }

    public void FinishGame()
    {
        _player.PlayerInput.PressedKeyMove -= OnPressedKeyMove;
        _player.PlayerInput.PressedKeyShoot -= OnPressedKeyShoot;
        _player.PlayerAttacker.UsedShoot -= OnUsedShoot;
        _player.Deathed -= OnDeathed;

        _player.gameObject.SetActive(false);
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