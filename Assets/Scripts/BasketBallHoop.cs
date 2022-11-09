using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class BasketBallHoop : MonoBehaviour
{
    [SerializeField] private Rim rim;
    [SerializeField] private Net net;
    private Input _input;

    private bool _isCurrent;

    public Action<bool> OnHit;

    public bool IsCurrent
    {
        get => _isCurrent;
        set
        {
            if (_isCurrent == value)
                return;
            _isCurrent = value;
            if (!value)
                StopAllCoroutines();
            else
            {
                StartCoroutine(RotateTowards()); ;
            }
        }
    }

    private void OnEnable()
    {
        ResetTransform();
    }

    public void ResetTransform()
    {
        transform.rotation = quaternion.identity;
        net.transform.localScale = Vector3.one;
    }

    public void Initialize(GameCore core, Input input)
    {
        _input = input;

        rim.Initialize(core);
        net.Initialize(this);
    }

    private IEnumerator RotateTowards()
    {
        while (_isCurrent)
        {
            Debug.Log($"_input.Distance.magnitude {_input.Distance.magnitude}");
            if (_input != null && _input.Distance.magnitude > 0)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.forward, _input.Distance);
                net.transform.localScale = Vector3.one + Vector3.up * Mathf.Clamp(_input.Distance.magnitude * .002f, 0, .5f);
            }

            yield return null;
        }
    }
}