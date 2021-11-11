using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPointHelper
{
    public static Vector2 GetRandomPointInCameraBounds(float offset)
    {
        var min = Camera.main.ScreenToWorldPoint(Vector2.zero);
        var xMax = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0));
        var yMax = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height));
        float screenX = Random.Range(min.x + offset, xMax.x - offset);
        float screenY = Random.Range(min.y + offset, yMax.y - offset);

        return new Vector2(screenX, screenY);
    }

    public static Vector2 GetRandomPointInGameObject(Transform transform, int offset)
    {
        var xMin = transform.position.x - offset;
        var xMax = transform.position.x + offset;
        var yMin = transform.position.y - offset;
        var yMax = transform.position.y + offset;
        float spawnX = Random.Range(xMin, xMax);
        float spawnY = Random.Range(yMin, yMax);
        
        return new Vector2(spawnX, spawnY);
    }

}
