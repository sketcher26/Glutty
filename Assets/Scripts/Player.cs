﻿using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private Movement movement;
    [SerializeField] private Shooting shooting;
    [SerializeField] private SpriteRenderer playerRenderer;
    [SerializeField] private PlayerSettings settings;
    [SerializeField] private UnityEvent growAction;
    [SerializeField] private LevelUpConfig levelUpConfig;
    [SerializeField] public float movementOffset;
    [SerializeField] private int currentLevel;
    public static int foodCount = 0;

    void Start()
    {
        SetSettings(levelUpConfig.GetLevelSettings(currentLevel));
    }
    
    void FixedUpdate()
    {
        PlayerMovement();
        PlayerRotation();
    }

    void PlayerRotation()
    {
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        movement.RotateTowardsTarget(target);
    }

    void PlayerMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        var direction = new Vector2(x, y);

        var pos = movement.Rb.position;
        var nextPos = pos + direction * movement.speed * Time.fixedDeltaTime;

        var xMax = movement.xMax - movementOffset;
        var yMax = movement.yMax - movementOffset;

        if (Mathf.Abs(nextPos.x) < 0 || Mathf.Abs(nextPos.x) >= xMax)
            x = 0;
        if (Mathf.Abs(nextPos.y) < 0 || Mathf.Abs(nextPos.y) >= yMax)
            y = 0;

        direction.x = x;
        direction.y = y;

        movement.MoveInDirection(direction);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2") && foodCount >= settings.maxFood && levelUpConfig.HasLevelSettings(currentLevel + 1))
        {
            if (growAction != null)
                growAction.Invoke();

            currentLevel += 1;
            var currentLevelSettings = levelUpConfig.GetLevelSettings(currentLevel);
            SetSettings(currentLevelSettings);

            foodCount = 0;
            ScoreCount.foodScore = 0;
        }

        if (Input.GetButtonDown("Fire1") && foodCount >= 1)
        {
            shooting.Shoot();
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        GameObject collidedWith = coll.gameObject;
        if (collidedWith.CompareTag("Food") && foodCount < settings.maxFood)
        {
            AddFood();
            FoodSpawn.Instance.DestroyFood(collidedWith);
        }
    }

    public void SetSettings(PlayerSettings newSettings)
    {
        settings = newSettings;
        transform.localScale = settings.scale;
        playerRenderer.color = settings.playerColor;
        movementOffset = settings.newMovementOffset;
        movement.SetSpeed(settings.speed);
    }

    public void AddFood()
    {
        if (foodCount < settings.maxFood)
        {
            ScoreCount.foodScore += 1;
            foodCount = foodCount + 1;
        }
    }

    public void RemoveFood()
    {
        if (foodCount >= 1)
        {
            ScoreCount.foodScore -= 1;
            foodCount -= 1;
        }
    }
}
