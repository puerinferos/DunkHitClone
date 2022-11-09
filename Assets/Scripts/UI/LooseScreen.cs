using System;
using UnityEngine;
using UnityEngine.UI;

public class LooseScreen : WindowBase
{
    [SerializeField] private Button restartBtn;

    private void Start()
    {
        restartBtn.onClick.AddListener(Restart);
    }

    private void Restart()
    {
        Close();
        _uiCore.OpenWindow<MenuScreen>();
    }
}