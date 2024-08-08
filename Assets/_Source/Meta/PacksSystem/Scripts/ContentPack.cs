using PingPonger.Gameplay;
using PingPonger.UI;
using UnityEngine;

namespace MetaGameplay
{
    [CreateAssetMenu(menuName = "Data/Meta/ContentPack", fileName = "ContentPack")]
    public class ContentPack : ScriptableObject
    {
        [SerializeField] private Color _wallColor;
        [SerializeField] private Platform _platformPrefab;
        [SerializeField] private BallFactory _ballFactory;
        [SerializeField] private AnimatedBackground _backgroundPrefab;
        [SerializeField] private FlyingText _flyingTextPrefab;

        public Color WallColor => _wallColor;
        public Platform PlatformPrefab => _platformPrefab;
        public BallFactory BallFactory => _ballFactory;
        public AnimatedBackground BackgroundPrefab => _backgroundPrefab;
        public FlyingText FlyingTextPrefab => _flyingTextPrefab;
    }
}
