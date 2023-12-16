using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RigidbodyLoopRotator : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _middleAngle = 90f;
    [SerializeField] private float _angleAmplitude = 15f;
    [SerializeField] private float _rotationSpeed = 10f;
    
    private Sequence _rotationSequence;
    private float _direction;

    private float LeftAngle => _middleAngle - _angleAmplitude;
    private float RightAngle => _middleAngle + _angleAmplitude;

    public float RotationSpeed
    {
        get => _rotationSpeed;
        set => _rotationSpeed = value;
    }

    private void OnEnable()
    {
        _direction = Random.Range(0, 2) == 0 ? -1 : 1;
    }

    private void OnDisable()
    {
        _rotationSequence?.Kill();
    }

    private void FixedUpdate()
    {
        _rigidbody.rotation += _direction * _rotationSpeed * Time.fixedDeltaTime;

        if (_rigidbody.rotation >= RightAngle && _direction > 0)
            _direction = -1;
        else if (_rigidbody.rotation <= LeftAngle && _direction < 0)
            _direction = 1;
    }
}
