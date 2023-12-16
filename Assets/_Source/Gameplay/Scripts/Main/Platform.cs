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
        public void SetJumpForce(float jumpForce) => _jumpPad.JumpForce = jumpForce;
        public RigidbodyLoopRotator PlatformRotator => _platformRotator;
    }
}