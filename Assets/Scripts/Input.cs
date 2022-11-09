using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Input
{
    private Vector2 _startPosition;
    private Vector2 _currentPosition;
    private Vector2 _distance;

    private float _maxMagnitude;

    public Action OnMouseDown;
    public Action<Vector2> OnMouse;
    public Action OnMouseUp;

    public Vector2 Distance => _distance;

    private bool isInputBlocked;

    public Input(float maxMagnitude)
    {
        _maxMagnitude = maxMagnitude;
    }

    public void BlockInput()
    {
        isInputBlocked = true;
    }

    public void UnBlockInput()
    {
        isInputBlocked = false;
    }

    public void DetectInput()
    {
        if(EventSystem.current.IsPointerOverGameObject() || isInputBlocked)
            return;
        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            _startPosition = UnityEngine.Input.mousePosition;
            _currentPosition = UnityEngine.Input.mousePosition + Vector3.up * .1f;

            OnMouseDown?.Invoke();
        }

        if (UnityEngine.Input.GetMouseButton(0))
        {
            _currentPosition = UnityEngine.Input.mousePosition;
            _distance = Vector2.ClampMagnitude(_startPosition - _currentPosition,_maxMagnitude);
            
            OnMouse?.Invoke(_distance);
        }

        if (UnityEngine.Input.GetMouseButtonUp(0))
        {
            BlockInput();
            OnMouseUp?.Invoke();
            _currentPosition = _startPosition;
            _distance = Vector2.ClampMagnitude(_startPosition - _currentPosition,_maxMagnitude);
        }
    }
}
