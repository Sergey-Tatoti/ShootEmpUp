using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Transform _attackPoint;
    private float _moveSpeed;
    private float _baseMagnitude = 0.25f;
    private Coroutine _coroutineMove;

    public event UnityAction<Enemy> ReachedPlace;

    public Transform AttackPoint => _attackPoint;

    public void Initialize(Rigidbody2D rigidBody2D, float moveSpeed)
    {
        _rigidbody2D = rigidBody2D;
        _moveSpeed = moveSpeed;
    }

    public void SetDestination(Transform attackPoint) => _attackPoint = attackPoint;

    public void ActivateMove() => _coroutineMove = StartCoroutine(Move());

    public void DeactivateMove() => StartCoroutine(Move());

    private IEnumerator Move()
    {
        Vector2 vector = (Vector2)_attackPoint.transform.position - (Vector2)transform.position;

        while (vector.magnitude > _baseMagnitude)
        {
            vector = (Vector2)_attackPoint.transform.position - (Vector2)transform.position;
            Vector2 direction = vector.normalized * Time.fixedDeltaTime;
            Vector2 nextPosition = _rigidbody2D.position + direction * _moveSpeed;

            _rigidbody2D.MovePosition(nextPosition);
            yield return null;
        }

        ReachedPlace?.Invoke(GetComponent<Enemy>());
    }
}