using UnityEngine;

public class CollectibleStar : MonoBehaviour, ICollectible
{
    public void Collect()
    {
        PlayerInfo.CurrentStars++;
        gameObject.SetActive(false);
    }
}