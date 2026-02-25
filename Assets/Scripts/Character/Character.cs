using ShootEmUp;
using UnityEngine;
using UnityEngine.Events;

public abstract class Character : MonoBehaviour, IHealth
{
    [SerializeField] protected Rigidbody2D _rigidbody2D;
    [SerializeField] protected CharacterCharacteristics _characterCharacteristics;

    protected int _currentHealth;

    public event UnityAction<Character> Deathed;

    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _characterCharacteristics.Health;
    public float MoveSpeed => _characterCharacteristics.MoveSpeed;

    public virtual void Initialize() 
    {
        _currentHealth = MaxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if(_currentHealth <= 0)
            Deathed?.Invoke(this);
    }
}