using UnityEngine;

namespace PingPonger.Gameplay
{
    public struct PlatfromBorders
    {
        private float _platformYPosition;
        private float _xLeftPosition;
        private float _xRightPosition;

        public PlatfromBorders(float platformYPosition, float xLeftPosition, float xRightPosition)
        {
            _platformYPosition = platformYPosition;
            _xLeftPosition = xLeftPosition;
            _xRightPosition = xRightPosition;
        }

        public float PlatformYPosition => _platformYPosition;
        public float ClampXPosition(float position, float size) => Mathf.Clamp(position, _xLeftPosition + size * 0.5f, _xRightPosition - size * 0.5f);
    }
}