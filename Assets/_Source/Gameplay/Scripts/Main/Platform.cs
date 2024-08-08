using UnityEngine;

namespace PingPonger.Gameplay
{
    [SelectionBase]
    public class Platform : MonoBehaviour
    {
        [SerializeField] private PlatformControl _platformControl;
        [SerializeField] private JumpPad _jumpPad;
        [SerializeField] private RigidbodyLoopRotator _platformRotator;

        public PlatformControl PlatformControl => _platformControl;
        public RigidbodyLoopRotator PlatformRotator => _platformRotator;

        public void SetJumpForce(float jumpForce) => _jumpPad.JumpForce = jumpForce;
        public void Enable() => _platformControl.enabled = true;
        public void Disable() => _platformControl.enabled = false;
    }
}