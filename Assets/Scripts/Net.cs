using UnityEngine;

public class Net : MonoBehaviour, IInteractable
{
    private BasketBallHoop _hoop;

    public void Initialize(BasketBallHoop hoop)
    {
        _hoop = hoop;
    }

    public void Interact()
    {
        _hoop.OnHit?.Invoke(_hoop.IsCurrent);
    }
}