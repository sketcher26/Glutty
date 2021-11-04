using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Settings", fileName = "Enemy Settings")]
public class EnemySettings : ScriptableObject
{
    public float speed;
    public Vector2 scale;
    public Color playerColor;
    public int maxFood;
    public float newMovementOffset;
    public Color enemyColor;
}

/*1. объеденить сеттинги
2.переделать playerManager (лвлап)
*/