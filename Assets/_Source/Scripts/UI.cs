using UnityEngine;

namespace View
{
    public class UI : MonoBehaviour
    {
        [Header("UI blocks")] [SerializeField] private MainMenuPresenter _mainMenuPresenter;
        [SerializeField] private SessionPresenter _sessionPresenter;

        public MainMenuPresenter MainMenuPresenter => _mainMenuPresenter;
        public SessionPresenter SessionPresenter => _sessionPresenter;
    }
}