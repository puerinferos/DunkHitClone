using UnityEngine;

public class LooseBorder : MonoBehaviour, IInteractable
{
    public GameCore _core;

    public void Initialize(GameCore core)
    {
        _core = core;
    }
    
    public void Interact()
    {
        _core.OnLoose?.Invoke();
    }
}