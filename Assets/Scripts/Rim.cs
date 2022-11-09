using UnityEngine;

public class Rim : MonoBehaviour, IInteractable
{
    private GameCore _core;

    public void Initialize(GameCore core)
    {
        _core = core;
    }

    public void Interact()
    {
        _core.ResetPerfect();
    }
}