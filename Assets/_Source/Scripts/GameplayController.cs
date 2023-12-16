using PingPonger.Gameplay;
using PingPonger.UI;
using UnityEngine;
using UnityEngine.UI;

namespace PingPonger.Gameplay
{ 
    public class GameplayController : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private SessionScoreView _sessionScoreView;
        [SerializeField] private GameContextInstaller _gameContextInstaller;
        [SerializeField] private SessionController _sessionController;

        [Space]
        [Min(1)][SerializeField] private int _scoreForSignleCollision;
        [Min(1)][SerializeField] private int _scoreForDoubleCollision;

        private GameContext _currentContext;
        private SessionScore _score;
        private BallsCollisionHandler _collisionHandler;

        private void OnEnable()
        {
            _playButton.onClick.AddListener(StartNewSession);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(StartNewSession);
        }

        private void StartNewSession()
        {
            _playButton.gameObject.SetActive(false);
            _currentContext = _gameContextInstaller.GetNewGameContext();
            _collisionHandler = new BallsCollisionHandler(_currentContext);

            _score = new SessionScore(_collisionHandler, _scoreForSignleCollision, _scoreForDoubleCollision);

            _sessionScoreView.Init(_score);
            _sessionScoreView.gameObject.SetActive(true);

            _sessionController.StartNewSession(_currentContext);
            _sessionController.SessionFinished += OnCurrentSessionFinished;
        }

        private void OnCurrentSessionFinished()
        {
            _playButton.gameObject.SetActive(true);
            _sessionScoreView.gameObject.SetActive(false);
            _sessionController.SessionFinished -= OnCurrentSessionFinished;

            _score.Dispose();
            _collisionHandler.Dispose();

            _currentContext.Clear();
            _currentContext.Dispose();
        }
    }
}
