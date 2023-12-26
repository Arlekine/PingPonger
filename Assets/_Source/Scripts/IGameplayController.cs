using System;

namespace PingPonger.Gameplay
{
    public interface IGameplayController
    {
        event Action<int> SessionCompleted;
        SessionContext StartNewSession();
    }
}