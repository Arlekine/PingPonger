using System;
using UnityEngine;

namespace PingPonger.Gameplay
{
    [SelectionBase]
    public class Ball : MonoBehaviour, IDestractable
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private TypedCollisionHandler<Collider2D> _collisionHandler;

        public event Action<Ball, Vector3, GameObject> Collided; 

        public Rigidbody2D Rigidbody => _rigidbody;

        public void DoDestroy()
        {
            Destroy(gameObject);
        }

        private void OnEnable()
        {
            _collisionHandler.CollisionEnter += OnCollided;
        }

        private void OnDisable()
        {
            _collisionHandler.CollisionEnter -= OnCollided;
        }

        private void OnCollided(Collider2D other, Collision2D collision)
        {
            Collided?.Invoke(this, collision.contacts[0].point, other.gameObject);
        }
    }
}