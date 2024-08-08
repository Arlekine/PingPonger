using System;
using PingPonger.UI;
using UnityEngine;
using UnityEngine.UI;

namespace PingPonger.Gameplay
{ 
    public class GameplayController : MonoBehaviour, IGameplayController
    {
        [SerializeField] private GameContextInstaller _gameContextInstaller;
        [SerializeField] private SessionController _sessionController;

        [Space]
        [Min(1)][SerializeField] private int _scoreForSignleCollision;
        [Min(1)][SerializeField] private int _scoreForDoubleCollision;

        private SessionContext _currentContext;
        private SessionScore _score;
        private BallsCollisionHandler _collisionHandler;

        public event Action<IServiceLocator<ISessionService>> NewSessionStarted;
        public event Action<IServiceLocator<ISessionService>> SessionContinued;
        public event Action Lost;
        public event Action<int> SessionCompleted;

        public void StartNewSession()
        {
            var sessionServiceLocator = new SessionServiceLocator();

            _currentContext = _gameContextInstaller.GetNewGameContext();
            _collisionHandler = new BallsCollisionHandler(_currentContext);

            _score = new SessionScore(_collisionHandler, _scoreForSignleCollision, _scoreForDoubleCollision);

            _sessionController.StartNewSession(_currentContext);
            _sessionController.Lost += OnLost;

            sessionServiceLocator.AddService(_currentContext);
            sessionServiceLocator.AddService(_collisionHandler);
            sessionServiceLocator.AddService(_score);

            NewSessionStarted?.Invoke(sessionServiceLocator);
        }

        public void ContinueCurrentSession()
        {
            _sessionController.Continue();
        }

        public int FinishCurrentSession()
        {
            _sessionController.Lost -= OnLost;

            var finalScore = _score.CurrentScore;

            _score.Dispose();
            _collisionHandler.Dispose();

            _currentContext.Clear();
            _currentContext.Dispose();

            SessionCompleted?.Invoke(finalScore);
            return finalScore;
        }

        private void OnLost()
        {
            Lost?.Invoke();
        }
    }
}
