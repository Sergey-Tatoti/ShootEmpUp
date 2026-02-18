using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] private EnemyManager _enemyManager;
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private LevelBackground _levelBackground;

        private void Awake()
        {
            _bulletSystem.Initialize();
            _playerManager.Initialize(_bulletSystem);
            _enemyManager.Initialize(_bulletSystem, _playerManager.Player.transform);

            _playerManager.DeathedPlayer += OnDeathedPlayer;
        }

        private void Start()
        {
            _enemyManager.ActivateEnemys();
        }

        private void Update()
        {
            _playerManager.UseActions();
        }

        private void FixedUpdate()
        {
            _playerManager.UseActionsFixedTime();
            _bulletSystem.UseActionsFixedTime();
            _levelBackground.MoveFixedTime();
        }

        private void OnDeathedPlayer() => FinishGame();

        public void FinishGame()
        {
            Debug.Log("Game over!");
            _enemyManager.DeactivateEnemys();
            Time.timeScale = 0;
        }
    }
}