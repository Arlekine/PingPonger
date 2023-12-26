using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PingPonger.Gameplay
{
    public class SessionContext : IDisposable
    {
        #region Data
        private Ball _ballPrefab;
        private Platform _platformPrefab;
        private PlatfromBorders _platfromBorders;
        private TypedTrigger<IDestractable> _outOfScreenTrigger;

        private Vector3 _ballCreationPoint;

        private Transform _gameParent;
        private Camera _mainCamera;
        private ClickInput _input;

        private BallsCollisionHandler _collisionHandler;
        private SessionScore _sessionScore;
        #endregion

        #region CurrentGame
        private Platform _currentPlatform;
        private readonly List<Ball> _currentBalls = new List<Ball>();
        #endregion

        public SessionContext(Ball ballPrefab, Platform platformPrefab, PlatfromBorders platfromBorders, 
            TypedTrigger<IDestractable> outOfScreenTrigger, Vector3 ballCreationPoint, 
            Transform gameParent, Camera mainCamera, ClickInput input,
            BallsCollisionHandler collisionHandler, SessionScore score)
        {
            _ballPrefab = ballPrefab;
            _platformPrefab = platformPrefab;
            _platfromBorders = platfromBorders;
            _outOfScreenTrigger = outOfScreenTrigger;
            _ballCreationPoint = ballCreationPoint;
            _gameParent = gameParent;
            _mainCamera = mainCamera;
            _input = input;

            _collisionHandler = collisionHandler;
            _sessionScore = score;

            _outOfScreenTrigger.TriggerEnter += OnSomethingDestroyed;
        }

        public event Action<Ball> BallCreated;
        public event Action<Ball> BallDestroyed;

        public List<Ball> CurrentBalls => _currentBalls;
        public Platform CurrentPlatform => _currentPlatform;
        public TypedTrigger<IDestractable> OutOfScreenTrigger => _outOfScreenTrigger;

        public BallsCollisionHandler CollisionHandler => _collisionHandler;
        public SessionScore SessionScore => _sessionScore;

        public Ball CreateNewBall()
        {
            var newBall = Object.Instantiate(_ballPrefab, _gameParent);
            newBall.transform.position = _ballCreationPoint;

            _currentBalls.Add(newBall);
            BallCreated?.Invoke(newBall);

            return newBall;
        }

        public Platform CreatePlatform()
        {
            if (_currentPlatform != null)
                throw new ArgumentException($"{nameof(Platform)} already exist in this context");

            _currentPlatform = Object.Instantiate(_platformPrefab, _gameParent);
            _currentPlatform.PlatformControl.Init(_input, _mainCamera, _platfromBorders);

            return _currentPlatform;
        }

        public void Clear()
        {
            foreach (var currentBall in CurrentBalls)
            {
                Object.Destroy(currentBall.gameObject);
            }

            if (_currentPlatform != null)
                Object.Destroy(_currentPlatform.gameObject);
        }

        public void Dispose()
        {
            _outOfScreenTrigger.TriggerEnter -= OnSomethingDestroyed;
        }

        private void OnSomethingDestroyed(IDestractable destractable)
        {
            if (destractable is Ball ball && _currentBalls.Remove(ball))
                BallDestroyed?.Invoke(ball);
        }
    }
}