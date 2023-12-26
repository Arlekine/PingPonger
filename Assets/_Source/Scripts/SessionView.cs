using PingPonger.Gameplay;
using PingPonger.UI;
using UnityEngine;

public class SessionView : MonoBehaviour
{
    [SerializeField] private SessionScoreView _sessionScoreView;

    public void Init(SessionContext sessionContext)
    {
        _sessionScoreView.Init(sessionContext.SessionScore);
        _sessionScoreView.gameObject.SetActive(true);
    }

    public void Clear()
    {
        _sessionScoreView.gameObject.SetActive(false);
    }
}