using System;
using PingPonger.Gameplay;
using UnityEngine;

namespace PingPonger.UI
{
    public class SessionScoreView : MonoBehaviour
    {
        private const string ScorePointsFormat = "+{0}";

        [SerializeField] private CounterView _scoreView;
        [SerializeField] private RectTransform _flyingTextParent;
        [SerializeField] private FlyingText _flyingScorePointsPrefab;

        private SessionScore _currentSessionScore;
        private Vector2 _scoreViewAnchoredPos;

        public void Init(SessionScore score)
        {
            _currentSessionScore = score;
            _scoreViewAnchoredPos = _scoreView.GetComponent<RectTransform>().anchoredPosition;

            _currentSessionScore.GotScore += OnGotScore;
            _scoreView.SetTextWithoutAnimation("0");
        }

        public void SetFlyingText(FlyingText flyingScorePointsPrefab)
        {
            _flyingScorePointsPrefab = flyingScorePointsPrefab;
        }

        private void OnGotScore(Vector3 scoreGettingWorldPos, int got, int actualScore)
        {
            var newFlyingText = Instantiate(_flyingScorePointsPrefab, _flyingTextParent);
            newFlyingText.FlyTo(String.Format(ScorePointsFormat, got), GetAnchoredPosForWorldPosition(scoreGettingWorldPos), _scoreViewAnchoredPos).onComplete += () => {_scoreView.SetText(actualScore.ToString());};
        }

        [EditorButton]
        private Vector2 GetAnchoredPosForWorldPosition(Vector3 scoreGettingWorldPos)
        {
            return _flyingTextParent.InverseTransformPoint(scoreGettingWorldPos);
        }
    }
}
