using System;
using System.Collections.Generic;
using UnityEngine;

namespace PingPonger.Gameplay
{
    public class BallsCollisionHandler : IDisposable
    {
        private GameContext _context;
        private readonly Dictionary<Ball, Ball> _doubleCollisions = new Dictionary<Ball, Ball>();

        public event Action<Vector3> BallCollided;
        public event Action<Vector3> TwoBallsCollided;

        public BallsCollisionHandler(GameContext context)
        {
            _context = context;
            _context.BallCreated += OnNewBallCreated;
            _context.BallDestroyed += OnNewBallCreated;
        }

        private void OnNewBallCreated(Ball ball)
        {
            ball.Collided += OnBallCollided;
        }

        private void OnBallDestroyed(Ball ball)
        {
            ball.Collided -= OnBallCollided;
        }

        private void OnBallCollided(Ball ball, Vector3 worldSpaceCollisionPoint, GameObject collidedWith)
        {
            var otherBall = collidedWith.GetComponent<Ball>();
            if (_doubleCollisions.ContainsKey(ball))
            {
                if (otherBall != null && _doubleCollisions[ball] == otherBall)
                {
                    _doubleCollisions.Remove(ball);
                    return;
                }
            }

            if (otherBall != null)
            {
                _doubleCollisions.Add(otherBall, ball);
                TwoBallsCollided?.Invoke(worldSpaceCollisionPoint);
            }
            else
            {
                BallCollided?.Invoke(worldSpaceCollisionPoint);
            }
        }

        public void Dispose()
        {
            foreach (var ball in _context.CurrentBalls)
            {
                ball.Collided -= OnBallCollided;
            }

            _doubleCollisions.Clear();
            _context.BallCreated -= OnNewBallCreated;
            _context.BallDestroyed -= OnNewBallCreated;
        }
    }
}