using System;

namespace ProgressionSystem
{
    public interface IProgress
    {
        event Action<float> Progressed;
    }
}
