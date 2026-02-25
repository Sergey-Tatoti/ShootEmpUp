using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class GameCycle : MonoBehaviour
    {
        static public GameState CurrentGameState;

        public enum GameState
        {
            None = 0,
            Start = 1,
            Playing = 2,
            Pause = 3,
            Resume = 4,
            Finish = 5,
        }

        private List<IGameListener> _gameListners;
        private List<IGameStartListener> _startListeners = new();
        private List<IGameUpdateListener> _updateListeners = new();
        private List<IGameFixedUpdateListener> _fixedUpdateListeners = new();
        private List<IGamePauseListener> _pauseListeners = new();
        private List<IGameResumeListener> _resumeListeners = new();
        private List<IGamePlayListener> _playListeners = new();
        private List<IGameFinishListener> _finishListeners = new();

        public void SetListeners(PlayerManager playerManager, EnemysManager enemysManager, BulletSystem bulletSystem, LevelBackground levelBackground)
        {
            _gameListners = new List<IGameListener>() { playerManager, enemysManager, bulletSystem, levelBackground, };

            for (int i = 0; i < _gameListners.Count; i++)
            {
                AddListener(_gameListners[i]);
            }
        }

        private void Start()
        {
            CurrentGameState = GameState.Start;

            for (int i = 0; i < _startListeners.Count; i++) { _startListeners[i].StartGame(); }
        }

        private void Update()
        {
            for (int i = 0; i < _updateListeners.Count; i++) { _updateListeners[i].UpdateGame(); }
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < _fixedUpdateListeners.Count; i++) { _fixedUpdateListeners[i].FixedUpdateGame(); }
        }

        public void PlayGame()
        {
            CurrentGameState = GameState.Playing;

            for (int i = 0; i < _playListeners.Count; i++) { _playListeners[i].PlayGame(); }
        }

        public void FinishGame()
        {
            CurrentGameState = GameState.Finish;

            for (int i = 0; i < _finishListeners.Count; i++) { _finishListeners[i].FinishGame(); }
        }

        public void PauseGame()
        {
            CurrentGameState = GameState.Pause;

            for (int i = 0; i < _pauseListeners.Count; i++) { _pauseListeners[i].PauseGame(); }
        }

        public void ResumeGame()
        {
            CurrentGameState = GameState.Resume;

            for (int i = 0; i < _resumeListeners.Count; i++) { _resumeListeners[i].ResumeGame(); }
        }

        private void AddListener(IGameListener listener)
        {
            if (listener is IGameStartListener startListener)
                _startListeners.Add(startListener);

            if (listener is IGameUpdateListener updateListener)
                _updateListeners.Add(updateListener);

            if (listener is IGameFixedUpdateListener fixedUpdateListener)
                _fixedUpdateListeners.Add(fixedUpdateListener);

            if (listener is IGamePlayListener playListener)
                _playListeners.Add(playListener);

            if (listener is IGamePauseListener pauseListener)
                _pauseListeners.Add(pauseListener);

            if (listener is IGameResumeListener resumeListener)
                _resumeListeners.Add(resumeListener);

            if (listener is IGameFinishListener finishListener)
                _finishListeners.Add(finishListener);
        }
    }
}