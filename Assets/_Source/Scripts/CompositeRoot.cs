using PingPonger.Gameplay;
using UnityEngine;

//ONLY DI
public class CompositeRoot : MonoBehaviour
{
    [SerializeField] private PlatformControl _platformControl;
    [SerializeField] private ClickInput _clickInput;
    [SerializeField] private Camera _mainCamera;
}