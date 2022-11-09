using System;
using UnityEngine;

public abstract class WindowBase : MonoBehaviour
{
    public Action OnOpen;
    public Action OnClose;

    protected UICore _uiCore;

    public void Initialize(UICore uiCore)
    {
        _uiCore = uiCore;
    }

    public virtual void Open()
    {
        gameObject.SetActive(true);
        OnOpen?.Invoke();
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
        OnClose?.Invoke();
    }
}