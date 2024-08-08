using UnityEngine;

namespace PingPonger.Gameplay
{
    public class EnvironmentCreator : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer[] _walls;
        [SerializeField] private Transform _backgroundParent;

        private AnimatedBackground _currentBackground;

        public void SetNewEnvironmentData(Color wallsColor, AnimatedBackground backgroundPrefab)
        {
            foreach (var wall in _walls)
            {
                wall.color = wallsColor;
            }

            if (_currentBackground != null)
                Destroy(_currentBackground.gameObject);

            _currentBackground = Instantiate(backgroundPrefab, _backgroundParent);
        }
    }
}