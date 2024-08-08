using System;
using Lean.Touch;
using ProgressionSystem;
using UnityEngine;
using Random = System.Random;

namespace PingPonger.Gameplay
{
    public class SessionController : MonoBehaviour
    {
        [SerializeField] private TimeProgress _timerProgress;

        [Header("Gravity progress")] 
        [SerializeField] private float _minGravityModifier = 1f;
        [SerializeField] private float _maxGravityModifier = 5f;

        [Header("Bouncing progress")]
        [SerializeField] private float _minJumpForceModifier = 10f;
        [SerializeField] private float _maxJumpForceModifier = 50f;

        [Header("Platform rotation progress")]
        [SerializeField] private float _minRotationSpeed = 2.5f;
        [SerializeField] private float _maxRotationSpeed = 5f;

        [Header("Ball spawn")] 
        [SerializeField] private AnimationCurve _maxBallsProgress;
        [SerializeField] private AnimationCurve _newBallsSpawnChancePerProgressUpdate;

        private SessionContext _currentContext;

        private LinearProgressProcessor _gravityProgress;
        private LinearProgressProcessor _bouncingProgress;
        private LinearProgressProcessor _platformRotationProgress;
        private CurveProgressProcessor _maxBallsProcessor;
        private CurveProgressProcessor _ballsSpawnChancesProcessor;

        public Action Lost;

        public void StartNewSession(SessionContext context)
        {
            _currentContext = context;
            _currentContext.BallCreated += OnBallCreated;
            _currentContext.BallDestroyed += OnBallDestroyed;

            _timerProgress.StartTimer();
            _gravityProgress = new LinearProgressProcessor(_timerProgress, _minGravityModifier, _maxGravityModifier, gravity => {_currentContext.CurrentBalls.ForEach(b => b.Rigidbody.gravityScale = gravity);});
            _bouncingProgress = new LinearProgressProcessor(_timerProgress, _minJumpForceModifier, _maxJumpForceModifier, force => {_currentContext.CurrentPlatform.SetJumpForce(force);});
            _platformRotationProgress = new LinearProgressProcessor(_timerProgress, _minRotationSpeed, _maxRotationSpeed, speed => {_currentContext.CurrentPlatform.PlatformRotator.RotationSpeed = speed;});
            _maxBallsProcessor = new CurveProgressProcessor(_timerProgress, _maxBallsProgress, balls => {});
            _ballsSpawnChancesProcessor = new CurveProgressProcessor(_timerProgress, _newBallsSpawnChancePerProgressUpdate, TrySpawnNewBall);

            _currentContext.CreatePlatform();
            _currentContext.CreateNewBall();
        }

        public void Continue()
        {
            _timerProgress.Continue();
            _currentContext.CreateNewBall();
            _currentContext.CurrentPlatform.Enable();
        }

        private void OnBallCreated(Ball ball)
        {
            ball.Rigidbody.gravityScale = _gravityProgress.Value;
        }

        private void OnBallDestroyed(Ball ball)
        {
            if (_currentContext.CurrentBalls.Count == 0)
            {
                _currentContext.CurrentPlatform.Disable();
                _timerProgress.Stop();
                Lost?.Invoke();
            }
        }

        private void TrySpawnNewBall(float currentSpawnChange)
        {
            if (_currentContext.CurrentBalls.Count < (int)_maxBallsProcessor.Value)
            {
                if (MathExtensions.Random.IsSuccess(_ballsSpawnChancesProcessor.Value))
                {
                    _currentContext.CreateNewBall();
                }
            }
        }
    }
}