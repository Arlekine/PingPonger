using UnityEngine;

namespace PingPonger.Gameplay
{
    [CreateAssetMenu(menuName = "Data/Gameplay/BallFactory", fileName = "BallFactory")]
    public class SimpleBallFactory : BallFactory
    {
        [SerializeField] private Ball _ballPrefab;

        public override Ball CreateBall(Transform parent)
        {
            return Instantiate(_ballPrefab, parent);
        }
    }
}