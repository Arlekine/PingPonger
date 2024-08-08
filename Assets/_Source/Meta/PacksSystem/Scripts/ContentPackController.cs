using PingPonger.Gameplay;
using PingPonger.UI;
using UnityEngine;

namespace MetaGameplay
{
    public class ContentPackController : MonoBehaviour
    {
        [SerializeField] private ContentPack[] _contentPacks;

        private GameContextInstaller _gameContextInstaller;
        private EnvironmentCreator _environmentCreator;
        private SessionScoreView _sessionScoreView;

        public void Init(GameContextInstaller gameContextInstaller, EnvironmentCreator environmentCreator, SessionScoreView sessionScoreView)
        {
            _gameContextInstaller = gameContextInstaller;
            _environmentCreator = environmentCreator;
            _sessionScoreView = sessionScoreView;

            SetPack(GetDefault());
        }

        public ContentPack GetDefault() => _contentPacks[0];

        [EditorButton]
        public void TestPackSetting()
        {
            SetPack(_contentPacks[1]);
        }

        private void SetPack(ContentPack pack)
        {
            _gameContextInstaller.SetConstructionElements(pack.PlatformPrefab, pack.BallFactory);
            _environmentCreator.SetNewEnvironmentData(pack.WallColor, pack.BackgroundPrefab);
            _sessionScoreView.SetFlyingText(pack.FlyingTextPrefab);
        }
    }
}