using System;

namespace MetaGameplay
{
    public class Currency
    {
        public Action<int, int> Added;
        public Action<int, int> Spent;

        private int _currentValue;

        public int CurrentValue => _currentValue;

        public void Add(int valueToAdd)
        {
            if (valueToAdd <= 0)
                throw new ArgumentException($"It is possible to add only positive value to {nameof(this.GetType)}");

            _currentValue += valueToAdd;
            Added?.Invoke(valueToAdd, _currentValue);
        }

        public void Spend(int valueToSpend)
        {
            if (valueToSpend <= 0)
                throw new ArgumentException($"It is possible to spend only positive value of {nameof(this.GetType)}");

            if (valueToSpend > _currentValue)
                throw new ArgumentException($"Not enough currency to spend");

            _currentValue -= valueToSpend;
            Spent?.Invoke(valueToSpend, _currentValue);
        }
    }
}
