using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace PingPonger.UI
{
    public class FlyingText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private UiShowingAnimation _appearAnimation;

        [Space]
        [SerializeField] private float _flightTime = 0.5f;
        [SerializeField] private Ease _flightEase = Ease.InCubic;

        private Sequence _flightSequence;

        [EditorButton]
        public Tween FlyTo(string text, Vector2 fromAnchoredPos, Vector2 toAnchoredPos)
        {
            if (_flightSequence != null)
                throw new ArgumentException("Attempt to start already flying text!");

            var rectTransform = GetComponent<RectTransform>();
            rectTransform.anchoredPosition = fromAnchoredPos;

            _appearAnimation.HideInstantly();
            _appearAnimation.Show();

            _text.text = text;

            _flightSequence = DOTween.Sequence();
            _flightSequence.Append(rectTransform.DOAnchorPos(toAnchoredPos, _flightTime).SetEase(_flightEase));
            _flightSequence.onComplete += () => Destroy(gameObject);
            return _flightSequence;
        }
    }
}