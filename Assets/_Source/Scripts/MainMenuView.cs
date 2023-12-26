using PingPonger.Gameplay;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private SessionView _sessionView;

    private IGameplayController _gameplayController;

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
        var context = _gameplayController.StartNewSession();
        _gameplayController.SessionCompleted += OnSessionComplete;
        _sessionView.Init(context);
    }

    private void OnSessionComplete(int sessionScore)
    {
        _gameplayController.SessionCompleted -= OnSessionComplete;
        _playButton.gameObject.SetActive(true);
        _sessionView.Clear();
    }
}