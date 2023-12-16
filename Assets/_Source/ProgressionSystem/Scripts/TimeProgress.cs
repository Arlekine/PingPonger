using System;
using UnityEngine;

namespace ProgressionSystem
{
    [Serializable]
    public class TimeProgress : MonoBehaviour, IProgress
    {
        [SerializeField] private float _progressDuration;
        [SerializeField] private float _updateRate = 0.1f;

        private float _startTime;
        private float _maxTime;

        private float _lastUpdate;

        public event Action<float> Progressed;

        public void StartTimer()
        {
            enabled = true;
            _startTime = Time.time;
            _maxTime = _startTime + _progressDuration;
        }

        public void Continue()
        {
            enabled = true;
        }

        public void Stop()
        {
            enabled = false;
        }

        private void Update()
        {
            if (Time.time <= _maxTime && Time.time - _lastUpdate >= _updateRate)
            {
                _lastUpdate = Time.time;
                var progress = 1f - (_maxTime - Time.time) / (_maxTime - _startTime);
                Progressed?.Invoke(Mathf.Clamp01(progress));
            }
        }
    }
}