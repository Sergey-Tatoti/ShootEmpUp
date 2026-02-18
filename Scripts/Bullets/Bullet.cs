using UnityEngine;
using UnityEngine.Events;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {
        [SerializeField] private new Rigidbody2D rigidbody2D;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private bool _isPlayer;
        private int _damage;
        private float _delay;

        public int Damage => _damage;

        public event UnityAction<Bullet, Collision2D> OnCollisionEntered;

        public void SetValues(Vector2 velocity, Vector3 position, Color color, int physicsLayer, int damage, float delay, bool isPlayer)
        {
            rigidbody2D.linearVelocity = velocity;
            transform.position = position;
            spriteRenderer.color = color;
            gameObject.layer = physicsLayer;
            _damage = damage;
            _delay = delay;
            _isPlayer = isPlayer;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEntered?.Invoke(this, collision);
        }
    }
}