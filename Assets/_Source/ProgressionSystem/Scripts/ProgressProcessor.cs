using System;

namespace ProgressionSystem
{
    public abstract class ProgressProcessor : IDisposable
    {
        private IProgress _currentProgress;

        public ProgressProcessor(IProgress progress)
        {
            _currentProgress = progress;
            _currentProgress.Progressed += OnProgressed;
        }

        protected abstract void OnProgressed(float progress);

        public void Dispose()
        {
            _currentProgress.Progressed -= OnProgressed;
        }
    }
}