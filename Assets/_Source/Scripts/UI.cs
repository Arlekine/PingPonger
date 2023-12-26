using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private MainMenuView _mainMenuView;
    [SerializeField] private SessionView _sessionView;

    public MainMenuView MainMenuView => _mainMenuView;
    public SessionView SessionView => _sessionView;
}