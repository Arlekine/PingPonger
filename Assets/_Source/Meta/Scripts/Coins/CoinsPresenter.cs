using MetaGameplay;
using UnityEngine;

namespace View
{
    public class CoinsPresenter : MonoBehaviour
    {
        [SerializeField] private CounterView _coinsView;

        private Currency _currentCoins;

        public void Init(Currency coins)
        {
            _currentCoins = coins;

            _coinsView.SetCounter(_currentCoins.CurrentValue);
            
            _currentCoins.Added += OnCoinsAdded;
            _currentCoins.Spent += OnCoinsSpent;
        }

        private void OnCoinsAdded(int added, int current)
        {
            _coinsView.SetCounter(current);
        }

        private void OnCoinsSpent(int spent, int current)
        {
            _coinsView.SetCounter(current);
        }

        private void OnDestroy()
        {
            _currentCoins.Added -= OnCoinsAdded;
            _currentCoins.Spent -= OnCoinsSpent;
        }
    }
}