using System;
using MetaGameplay;

namespace App
{
    public class CoinsInteractor : IDisposable
    {
        private DataLoader _dataLoader;
        private Currency _coinsCurrency;

        public CoinsInteractor(DataLoader dataLoader, Currency coinsPresenter)
        {
            _dataLoader = dataLoader;
            _coinsCurrency = coinsPresenter;

            _coinsCurrency.ValueChanged += OnCoinsChanged;
        }

        private void OnCoinsChanged(int currentCoins)
        {
            _dataLoader.CurrentGameData.CurrentCoins = currentCoins;
            _dataLoader.Save();
        }

        public void Dispose()
        {
            _coinsCurrency.ValueChanged -= OnCoinsChanged;
        }
    }
}