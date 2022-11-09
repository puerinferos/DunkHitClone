using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameScreen : WindowBase
{
    [SerializeField] private TMP_Text counter;
    [SerializeField] private TMP_Text starsCounter;
    [SerializeField] private Button pauseBtn;

    private void Start()
    {
        pauseBtn.onClick.AddListener(OpenPause);
        PlayerInfo.OnStarsUpdated += ()=> UpdateStarsCounter(PlayerInfo.CurrentStars);

        _uiCore.GetWindow<LooseScreen>().OnClose += Close;
    }

    private void OpenPause()
    {
        _uiCore.OpenWindow<PauseScreen>();
    }

    public void UpdateCounter(int count)
    {
        counter.text = count.ToString();
    }

    private void UpdateStarsCounter(int count)
    {
        starsCounter.text = count.ToString();
    }
}