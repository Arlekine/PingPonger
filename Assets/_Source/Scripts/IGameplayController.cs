using System;

namespace PingPonger.Gameplay
{
    public interface IGameplayController
    {
        event Action<IServiceLocator<ISessionService>> NewSessionStarted;
        event Action<IServiceLocator<ISessionService>> SessionContinued;
        event Action Lost;
        event Action<int> SessionCompleted;

        void StartNewSession();
        void ContinueCurrentSession();
        int FinishCurrentSession();
    }
}