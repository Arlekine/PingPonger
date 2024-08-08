using MetaGameplay;
using PingPonger.Gameplay;
using PingPonger.UI;
using UnityEngine;


namespace View
{
    public class SessionPresenter : MonoBehaviour
    {
        [SerializeField] private SessionScoreView _sessionScoreView;

        [Space] 
        [SerializeField] private RectTransform _popUpsParent;
        [SerializeField] private AcceptPopUp _continuePopUp;
        [SerializeField] private AcceptPopUp _gameOverPopUp;

        public SessionScoreView SessionScoreView => _sessionScoreView;

        private IGameplayController _gameplayController;
        private GameSessionRewardManager _rewardManager;

        private AcceptPopUp _currentContinuePopUp;
        private AcceptPopUp _currentGameOverPopUp;

        private bool _isLostBefore;
        private bool _isRewardDoubled;

        public void Init(IGameplayController gameplayController, GameSessionRewardManager rewardManager)
        {
            _gameplayController = gameplayController;
            _rewardManager = rewardManager;

            _gameplayController.NewSessionStarted += CreateUIForSession;
            _gameplayController.Lost += OnLost;
        }

        public void CreateUIForSession(IServiceLocator<ISessionService> sessionSL)
        {
            _isLostBefore = false;

            _sessionScoreView.Init(sessionSL.GetService<SessionScore>());
            _sessionScoreView.gameObject.SetActive(true);
        }

        private void OnLost()
        {
            if (_isLostBefore == false)
            {
                _isLostBefore = true;
                _currentContinuePopUp = Instantiate(_continuePopUp, _popUpsParent);
                _currentContinuePopUp.Open(OnContinueAccepted, ShowGameOverPopUp);
            }
            else
            {
                ShowGameOverPopUp();
            }
        }

        private void OnContinueAccepted()
        {
            _currentContinuePopUp.CloseAndDestroy();
            _gameplayController.ContinueCurrentSession();
        }

        private void ShowGameOverPopUp()
        {
            _sessionScoreView.gameObject.SetActive(false);
            _currentContinuePopUp.CloseAndDestroy();

            _currentGameOverPopUp = Instantiate(_gameOverPopUp, _popUpsParent);
            _currentGameOverPopUp.Open(ShowRewaredAd, EndSession);
        }

        private void ShowRewaredAd()
        {
            //show ad

            _isRewardDoubled = true;
            EndSession();
        }

        private void EndSession()
        {
            var finalScore = _gameplayController.FinishCurrentSession();

            if (_isRewardDoubled)
                finalScore *= 2;

            _currentGameOverPopUp.CloseAndDestroy();
            _rewardManager.AddCoinsForScore(finalScore);
        }
    }
}