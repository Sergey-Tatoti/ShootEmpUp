using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private float _speedMove;
    private float _directionMove;

    public void Initialize(Rigidbody2D rigidbody2D, float speedMove)
    {
        _rigidbody2D = rigidbody2D;
        _speedMove = speedMove;
    }

    public void SetDirectionMove(float directionMove) => _directionMove = directionMove;

    public void Move()
    {
        Vector2 nextPosition = _rigidbody2D.position + (new Vector2(_directionMove, 0) * _speedMove * Time.fixedDeltaTime);

        _rigidbody2D.MovePosition(nextPosition);
    }
}