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

    public static Vector2 GetRandomPointAroundGameObject(Transform transform, int radius)
    {
        return (Vector2)transform.position + Random.insideUnitCircle * radius;
    }

}
