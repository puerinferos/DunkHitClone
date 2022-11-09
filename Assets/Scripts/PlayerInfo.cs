using System;
using UnityEngine;

public static class PlayerInfo
{
    public static int CurrentStars
    {
        get => PlayerPrefs.GetInt("CurrentStars");
        set
        {
            OnStarsUpdated?.Invoke();
            PlayerPrefs.SetInt("CurrentStars",value);
        } 

    }
    public static int MaxCounter
    {
        get => PlayerPrefs.GetInt("MaxCounter");
        set => PlayerPrefs.SetInt("MaxCounter",value);
    }

    public static Action OnStarsUpdated;
}