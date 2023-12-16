using System;

public static class MathExtensions
{
    public static class Random 
    {
        public static bool IsSuccess(float normalizedChance)
        {
            if (normalizedChance < 0 || normalizedChance > 1)
                throw new ArgumentException($"{nameof(normalizedChance)} should be between 0 and 1 inclusive");

            var random = UnityEngine.Random.Range(0f, 1f);
            return random <= normalizedChance;
        }
    }
}