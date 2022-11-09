using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : WindowBase
{
    [SerializeField] private Button mainMenuBtn;
    [SerializeField] private Button resumeBtn;

    private void Start()
    {
        mainMenuBtn.onClick.AddListener(OpenMainMenu);
        resumeBtn.onClick.AddListener(Resume);
    }

    private void OpenMainMenu()
    {
        _uiCore.OpenWindow<MenuScreen>();
        Close();
    }

    private void Resume()
    {
        Close();
    }
}