using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private float foodCount = 0;
    [SerializeField] private PlayerMove movement;
    [SerializeField] private SpriteRenderer playerRenderer;
    [SerializeField] private PlayerSettings settings;
    [SerializeField] private UnityEvent growAction;

    public void SetSettings(PlayerSettings newSettings)
    {
        settings = newSettings;
        transform.localScale = settings.scale;
        playerRenderer.color = settings.playerColor;
        movement.SetSpeed(settings.speed);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && foodCount >= settings.maxFood)
        {
            if(growAction != null)
                growAction.Invoke();

            foodCount = 0;
            ScoreCount.foodScore = 0;
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        GameObject collidedWith = coll.gameObject;
        if (collidedWith.CompareTag("Food") && foodCount < settings.maxFood)
        {
            ScoreCount.foodScore += 1;
            foodCount = foodCount + 1;
            Destroy(collidedWith);
        }
    }
}
