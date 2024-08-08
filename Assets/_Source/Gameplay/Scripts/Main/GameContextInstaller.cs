using UnityEngine;

namespace PingPonger.Gameplay
{
    public class GameContextInstaller : MonoBehaviour
    {
        [SerializeField] private Transform _gameParent;
        [SerializeField] private Transform _platfromPoint;
        [SerializeField] private Transform _platfromLeftMoveBorder;
        [SerializeField] private Transform _platfromRightMoveBorder;
        [SerializeField] private Transform _ballSpawnPoint;

        [Space]
        [SerializeField] private Camera _camera;
        [SerializeField] private ClickInput _input;

        [Space]
        [SerializeField] private TypedTrigger<IDestractable> _outOfScreenDestractor;

        private BallFactory _currentBallFactory;
        private Platform _currentPlatformPrefab;

        public void SetConstructionElements(Platform platformPrefab, BallFactory ballFactory)
        {
            _currentBallFactory = ballFactory;
            _currentPlatformPrefab = platformPrefab;
        }

        [EditorButton]
        public SessionContext GetNewGameContext()
        {
            var borders = new PlatfromBorders(_platfromPoint.position.y, 
                _platfromLeftMoveBorder.position.x,
                _platfromRightMoveBorder.position.x);

            return new SessionContext(_currentBallFactory, _currentPlatformPrefab, borders, _outOfScreenDestractor, _ballSpawnPoint.position, _gameParent, _camera, _input);
        }
    }
}