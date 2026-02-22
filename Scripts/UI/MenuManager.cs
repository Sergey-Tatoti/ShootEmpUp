using ShootEmUp;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button _buttonPlay;
    [SerializeField] private Button _buttonPause;
    [SerializeField] private Button _buttonResume;
    [SerializeField] private TMP_Text _textTimePlay;

    public event UnityAction<GameCycle.GameState> ClickedButtonState;

    public void Initialize()
    {
        _buttonPlay.gameObject.SetActive(true);
        _buttonPause.gameObject.SetActive(false);
        _buttonResume.gameObject.SetActive(false);

        _buttonPlay.onClick.AddListener(OnClickedButtonPlay);
        _buttonPause.onClick.AddListener(OnClickedButtonPause);
        _buttonResume.onClick.AddListener(OnClickedButtonResume);
    }

    public void ShowTextTimePlay(int time)
    {
        _textTimePlay.gameObject.SetActive(time > 0);
        _textTimePlay.text = time.ToString();
    }

    private void OnClickedButtonPlay()
    {
        _buttonPlay.gameObject.SetActive(false);
        _buttonPause.gameObject.SetActive(true);
        ClickedButtonState?.Invoke(GameCycle.GameState.Playing);
    }

    private void OnClickedButtonPause()
    {
        ShowButtonPause(true);
        ClickedButtonState?.Invoke(GameCycle.GameState.Pause);
    }

    private void OnClickedButtonResume()
    {
        ShowButtonPause(false);
        ClickedButtonState?.Invoke(GameCycle.GameState.Resume);
    }

    private void ShowButtonPause(bool isShowPause)
    {
        _buttonResume.gameObject.SetActive(isShowPause);
        _buttonPause.gameObject.SetActive(!isShowPause);
    }
}