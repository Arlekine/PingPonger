using System;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class AcceptPopUp : MonoBehaviour
    {
        [SerializeField] private Button _acceptButton;
        [SerializeField] private Button _declineButton;
        [SerializeField] private UiShowingAnimation _showingAnimation;

        private Action _onAccepted;
        private Action _onDeclined;

        public void Open(Action onAccepted, Action onDeclined)
        {
            _showingAnimation.Show();

            _onAccepted = onAccepted;
            _onDeclined = onDeclined;
        }

        public void CloseAndDestroy()
        {
            _showingAnimation.Hide();
            Destroy(gameObject, 0.3f);
        }

        private void OnEnable()
        {
            _acceptButton.onClick.AddListener(OnAdClicked);
            _declineButton.onClick.AddListener(OnContinueClicked);
        }

        private void OnDisable()
        {
            _acceptButton.onClick.RemoveListener(OnAdClicked);
            _declineButton.onClick.RemoveListener(OnContinueClicked);
        }

        private void OnAdClicked() => _onAccepted?.Invoke();
        private void OnContinueClicked() => _onDeclined?.Invoke();
    }
}