using System;
using UnityEngine;

namespace PingPonger.Gameplay
{
    public class SessionScore : ISessionService, IDisposable
    {
        private int _currentScore;
        private BallsCollisionHandler _collisionHandler;

        private int _scoreForSignle;
        private int _scoreForDouble;

        public Action<Vector3, int, int> GotScore;

        public int CurrentScore => _currentScore;

        public SessionScore(BallsCollisionHandler collisionHandler, int scoreForSingleCollision, int scoreForDoubleCollision)
        {
            _collisionHandler = collisionHandler;
            _scoreForSignle = scoreForSingleCollision;
            _scoreForDouble = scoreForDoubleCollision;

            _collisionHandler.BallCollided += OnBallCollided;
            _collisionHandler.TwoBallsCollided += OnTwoBallsCollided;
        }

        private void OnBallCollided(Vector3 worldSpaceCollisionPoint)
        {
            _currentScore += _scoreForSignle;
            GotScore?.Invoke(worldSpaceCollisionPoint, _scoreForSignle, _currentScore);
        }
        private void OnTwoBallsCollided(Vector3 worldSpaceCollisionPoint)
        {
            _currentScore += _scoreForDouble;
            GotScore?.Invoke(worldSpaceCollisionPoint, _scoreForDouble, _currentScore);
        }

        public void Dispose()
        {
            _collisionHandler.BallCollided -= OnBallCollided;
            _collisionHandler.TwoBallsCollided -= OnTwoBallsCollided;
        }
    }
}
