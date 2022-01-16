using UnityEngine;

[CreateAssetMenu(menuName = "Entity/Settings", fileName = "Entity Settings")]
public class EntitySettings : ScriptableObject
{
    public float speed;
    public Vector2 scale;
    public Color playerColor;
    public float maxFood;
    public float newMovementOffset;
    public int hitPoints;
}
