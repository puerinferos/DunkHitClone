using UnityEngine;

[CreateAssetMenu(fileName = "CoreContext", menuName = "CoreContext", order = 0)]
public class CoreContext : ScriptableObject
{
    public int hoopStartCount = 5;
    public Vector2 hoopStartPosition;
    public Vector2 ballStartPosition;
    public Vector2 yOffsetMinMax;
    public float maxInputRange;
    public float starChance;
}