using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;

public class ClickInput : MonoBehaviour
{
    private LeanFinger _currentFinger;

    public event Action<Vector2> Pressed;
    public event Action<Vector2> Released;

    public bool IsPressed => _currentFinger != null;

    public Vector2 CurrentPointerPosition
    {
        get
        {
            if (IsPressed == false)
                throw new Exception("Can't get pointer position, because pointer isn't pressed");

            return _currentFinger.LastScreenPosition;
        }
    }

    private void OnEnable()
    {
        _currentFinger = null;

        LeanTouch.OnFingerDown += FingerPress;
        LeanTouch.OnFingerUp += FingerUp;
    }

    private void OnDisable()
    {
        LeanTouch.OnFingerDown -= FingerPress;
        LeanTouch.OnFingerUp -= FingerUp;
    }

    private void FingerPress(LeanFinger finger)
    {
        if (_currentFinger == null && finger.StartedOverGui == false)
        {
            _currentFinger = finger;
            Pressed?.Invoke(finger.ScreenPosition);
        }
    }

    private void FingerUp(LeanFinger finger)
    {
        if (_currentFinger == finger)
        {
            _currentFinger = null;
            Released?.Invoke(finger.ScreenPosition);
        }
    }
}
