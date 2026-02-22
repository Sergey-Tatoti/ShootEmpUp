using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameManager : MonoBehaviour
    {
        [SerializeField] private GameCycle _gameCycle;
        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] private EnemysManager _enemyManager;
        [SerializeField] private MenuManager _menuManager;
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private LevelBackground _levelBackground;

        private void Awake()
        {
            _gameCycle.SetListeners(_playerManager, _enemyManager, _bulletSystem, _levelBackground);

            _playerManager.Initialize(_bulletSystem);
            _enemyManager.Initialize(_bulletSystem, _playerManager.Player.transform);
            _menuManager.Initialize();
            _bulletSystem.Initialize();

            _playerManager.DeathedPlayer += OnDeathedPlayer;
            _menuManager.ClickedButtonState += OnClickedButtonState;
        }

        private void OnDeathedPlayer()
        {
            _gameCycle.FinishGame();
            Debug.Log("Game over!");
        }

        private void OnClickedButtonState(GameCycle.GameState gameState)
        {
            switch(gameState)
            {
                case GameCycle.GameState.Playing:
                    _gameCycle.PlayGame();
                    break;
                case GameCycle.GameState.Resume:
                    _gameCycle.ResumeGame();
                    break;
                case GameCycle.GameState.Pause:
                    _gameCycle.PauseGame();
                    break;
            }
        }
    }
}