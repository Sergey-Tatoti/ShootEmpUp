using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    private int _health;

    public event UnityAction HealthedEmpty;

    public void SetStats(int health)
    {
        _health = health;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if(_health <= 0)
            HealthedEmpty?.Invoke();
    }
}