using PingPonger.Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class MainMenuPresenter : MonoBehaviour
    {
        [SerializeField] private Button _playButton;

        [Header("Child presenters")] [SerializeField]
        private CoinsPresenter _coinsPresenter;

        private IGameplayController _gameplayController;

        public CoinsPresenter CoinsPresenter => _coinsPresenter;

        public void Init(IGameplayController controller)
        {
            _gameplayController = controller;
        }

        private void OnEnable()
        {
            _playButton.onClick.AddListener(OnStart);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(OnStart);
        }

        private void OnStart()
        {
            _playButton.gameObject.SetActive(false);
            _gameplayController.StartNewSession();
            _gameplayController.SessionCompleted += OnSessionComplete;
        }

        private void OnSessionComplete(int sessionScore)
        {
            _gameplayController.SessionCompleted -= OnSessionComplete;
            _playButton.gameObject.SetActive(true);
        }
    }
}