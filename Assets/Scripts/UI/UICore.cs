using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class UICore : MonoBehaviour
{
    [SerializeField] private List<WindowBase> windows = new List<WindowBase>();

    private void Awake()
    {
        foreach (WindowBase window in windows)
        {
            window.Initialize(this);
        }
        OpenWindow<MenuScreen>();
        GetWindow<MenuScreen>().OnClose += OpenWindow<InGameScreen>;
    }

    public void OpenWindow<T>() where T : WindowBase
    {
        var windowBase = windows.First(x => x.GetType() == typeof(T));

        if (windowBase != null)
            windowBase.Open();
    }

    public T GetWindow<T>() where T : WindowBase
    {
        T neededWindow = (T)windows.First(x => x.GetType() == typeof(T));

        return neededWindow;
    }

    public void CloseWindow<T>() where T : WindowBase
    {
        var windowBase = windows.First(x => x.GetType() == typeof(T));
        windowBase.Close();
    }
}