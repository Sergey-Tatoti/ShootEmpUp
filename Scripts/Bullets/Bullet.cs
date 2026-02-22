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
        private Vector3 _velocity;

        public int Damage => _damage;
        public Vector3 Velocity => _velocity;

        public event UnityAction<Bullet, Collision2D> OnCollisionEntered;

        public void SetValues(Vector2 velocity, Vector3 position, Color color, int physicsLayer, int damage, float delay, bool isPlayer)
        {
            _velocity = velocity;
            rigidbody2D.linearVelocity = velocity;
            transform.position = position;
            spriteRenderer.color = color;
            gameObject.layer = physicsLayer;
            _damage = damage;
            _delay = delay;
            _isPlayer = isPlayer;
        }

        public void Activate(bool isActivate)
        {
            rigidbody2D.linearVelocity = isActivate ? _velocity : Vector3.zero;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEntered?.Invoke(this, collision);
        }
    }
}