using UnityEngine;

namespace MetaGameplay
{
    public class GameSessionRewardManager : MonoBehaviour
    {
        [SerializeField] private float _scoreToCoinsConversionRate;

        private Currency _coins;

        public void Init(Currency coins)
        {
            _coins = coins;
        }

        public void AddCoinsForScore(int finalScore)
        {
            var coinsToAdd = Mathf.RoundToInt(_scoreToCoinsConversionRate * finalScore);
            _coins.Add(coinsToAdd);
        }
    }
}