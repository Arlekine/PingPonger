using PingPonger.Gameplay;
using UnityEngine;

//ONLY DI
public class CompositeRoot : MonoBehaviour
{
    [SerializeField] private PlatformControl _platformControl;
    [SerializeField] private ClickInput _clickInput;
    [SerializeField] private GameplayController _gameplayController;
    [SerializeField] private UI _ui;
}