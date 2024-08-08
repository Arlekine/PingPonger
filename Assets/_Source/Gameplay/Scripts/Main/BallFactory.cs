using UnityEngine;

namespace PingPonger.Gameplay
{
    public abstract class BallFactory : ScriptableObject
    {
        public abstract Ball CreateBall(Transform parent);
    }
}