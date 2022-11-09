using UnityEngine;

public class Border : MonoBehaviour, IInteractable
{
    public GameCore _core;

    public void Initialize(GameCore core)
    {
        _core = core;
    }

    public void Interact()
    {
        _core.ResetPerfect();
    }
}