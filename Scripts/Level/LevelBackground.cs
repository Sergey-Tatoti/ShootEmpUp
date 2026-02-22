using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBackground : MonoBehaviour, IGameFixedUpdateListener
    {
        [SerializeField] private float _startPositionY;
        [SerializeField] private float _endPositionY;
        [SerializeField] private float _movingSpeedY;

        public void FixedUpdateGame()
        {
            if (GameCycle.CurrentGameState == GameCycle.GameState.Playing || GameCycle.CurrentGameState == GameCycle.GameState.Resume)
                Move();
        }

        private void Move()
        {
            if (transform.position.y <= _endPositionY)
                transform.position = new Vector3(transform.position.x, _startPositionY, transform.position.z);

            transform.position -= new Vector3(transform.position.x, _movingSpeedY * Time.fixedDeltaTime, transform.position.z);
        }
    }
}