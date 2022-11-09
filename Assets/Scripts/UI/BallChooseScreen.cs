using System;

public class BallChooseScreen : WindowBase
{
    public Action OnBallChange;
    
    public void ChangeBall()
    {
        OnBallChange?.Invoke();
    }
}