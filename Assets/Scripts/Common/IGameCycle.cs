using ShootEmUp;

public interface IGameListener { }

interface IGameStartListener : IGameListener
{
    public void StartGame();
}

interface IGamePlayListener : IGameListener
{
    public void PlayGame();
}

interface IGameFinishListener : IGameListener
{
    public void FinishGame();
}

interface IGamePauseListener : IGameListener
{
    public void PauseGame();
}

interface IGameResumeListener : IGameListener
{
    public void ResumeGame();
}

interface IGameUpdateListener : IGameListener
{
    public void UpdateGame();
}

interface IGameFixedUpdateListener : IGameListener
{
    public void FixedUpdateGame();
}