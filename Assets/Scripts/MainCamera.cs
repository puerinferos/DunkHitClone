using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private float maxY;
    private Transform _lookAt;

    public void ResetPosition()
    {
    transform.position = Vector3.back * 10;    
    }
    
    public void Initialize(Transform lookAt)
    {
        _lookAt = lookAt;
    }

    private void LateUpdate()
    {
        if (_lookAt.position.y - transform.position.y > maxY)
            transform.position += (Vector3.up * _lookAt.position.y) * Time.deltaTime;
    }
}