using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : WindowBase
{
    [SerializeField] private Button ballChooseBtn;

    private void Start()
    {
        ballChooseBtn.onClick.AddListener(OpenBallChoosePanel);
    }

    private void OpenBallChoosePanel()
    {
        _uiCore.OpenWindow<BallChooseScreen>();
    }
}