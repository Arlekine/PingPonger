using UnityEngine;

namespace PingPonger.Gameplay
{
    public class PlatformControl : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider2D;

        private ClickInput _input;
        private Camera _camera;

        private PlatfromBorders _platfromBorders;

        public void Init(ClickInput input, Camera camera, PlatfromBorders borders)
        {
            _platfromBorders = borders;

            _input = input;
            _camera = camera;
        }

        private void Update()
        {
            if (_input.IsPressed)
            {
                var clickPos = _camera.ScreenToWorldPoint(_input.CurrentPointerPosition);

                clickPos.x = _platfromBorders.ClampXPosition(clickPos.x, _collider2D.bounds.size.x);
                clickPos.y = _platfromBorders.PlatformYPosition;
                clickPos.z = 0;

                transform.position = clickPos;
            }
        }
    }
}
