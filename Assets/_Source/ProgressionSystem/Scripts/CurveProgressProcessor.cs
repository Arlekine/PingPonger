using System;
using UnityEngine;

namespace ProgressionSystem
{
    public class CurveProgressProcessor : ProgressProcessor
    {
        private AnimationCurve _spawnCurve;

        private float _value;

        private Action<float> _progressProcessor;

        public CurveProgressProcessor(IProgress progress, AnimationCurve curve, Action<float> progressProcessor) : base(progress)
        {
            _spawnCurve = curve;
            _progressProcessor = progressProcessor;
            _value = GetProgressValue(0f);
        }

        public float Value => _value;

        protected override void OnProgressed(float progress)
        {
            _value = _spawnCurve.Evaluate(progress);
            _progressProcessor(_value);
        }

        private float GetProgressValue(float progress) => _spawnCurve.Evaluate(progress);
    }
}