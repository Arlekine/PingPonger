using System;
using UnityEngine;

namespace ProgressionSystem
{
    public class LinearProgressProcessor : ProgressProcessor
    {
        private float _minValue;
        private float _maxValue;

        private float _value;

        private Action<float> _progressProcessor;

        public LinearProgressProcessor(IProgress progress, float minValue, float maxValue, Action<float> progressProcessor) : base(progress)
        {
            _minValue = minValue;
            _maxValue = maxValue;
            _progressProcessor = progressProcessor;
            _value = GetProgressValue(0f);
        }

        public float Value => _value;

        protected override void OnProgressed(float progress)
        {
            _value = GetProgressValue(progress);
            _progressProcessor(_value);
        }

        private float GetProgressValue(float progress) => Mathf.Lerp(_minValue, _maxValue, progress);
    }
}