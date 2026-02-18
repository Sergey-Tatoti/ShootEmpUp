using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private KeyCode _keyShoot;

    public event UnityAction PressedKeyShoot;
    public event UnityAction<float> PressedKeyMove;

    public void TryInputKeyCodes()
    {
        TryInputKeyMove();
        TryInputKeyShoot();
    }

    private void TryInputKeyShoot()
    {
        if (Input.GetKey(_keyShoot))
            PressedKeyShoot?.Invoke();
    }

    private void TryInputKeyMove()
    {
        float directionMove = Input.GetAxis("Horizontal");

        if (directionMove != 0)
            PressedKeyMove?.Invoke(directionMove);
    }
}