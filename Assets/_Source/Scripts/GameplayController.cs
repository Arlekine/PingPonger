using System;
using PingPonger.UI;
using UnityEngine;
using UnityEngine.UI;

namespace PingPonger.Gameplay
{ 
    public class GameplayController : IGameplayController
    {
        [SerializeField] private GameContextInstaller _gameContextInstaller;
        [SerializeField] private SessionController _sessionController;

        [Space]
        [Min(1)][SerializeField] private int _scoreForSignleCollision;
        [Min(1)][SerializeField] private int _scoreForDoubleCollision;

        private SessionContext _currentContext;
        private SessionScore _score;
        private BallsCollisionHandler _collisionHandler;

        public event Action<int> SessionCompleted;

        public SessionContext StartNewSession()
        {
            _currentContext = _gameContextInstaller.GetNewGameContext();
            _collisionHandler = new BallsCollisionHandler(_currentContext);

            _score = new SessionScore(_collisionHandler, _scoreForSignleCollision, _scoreForDoubleCollision);

            _sessionController.StartNewSession(_currentContext);
            _sessionController.SessionFinished += OnCurrentSessionFinished;
            return _currentContext;
        }

        private void OnCurrentSessionFinished()
        {
            _sessionController.SessionFinished -= OnCurrentSessionFinished;

            _score.Dispose();
            _collisionHandler.Dispose();

            _currentContext.Clear();
            _currentContext.Dispose();
        }
    }
}
