using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Settings", fileName = "Player Settings")]
public class PlayerSettings : ScriptableObject
{
    public float speed;
    public Vector2 scale;
    public Color playerColor;
    public int maxFood;
    public float newMovementOffset;
}
