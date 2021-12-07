using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private Movement movement;
    [SerializeField] private Shooting shooting;
    [SerializeField] private SpriteRenderer playerRenderer;
    [SerializeField] private EntitySettings settings;
    [SerializeField] private LevelUpConfig levelUpConfig;
    [SerializeField] public float movementOffset;
    [SerializeField] private int currentLevel;
    public static int foodCount = 0;
    public float MaxFood
    {
        get
        {
            return settings.maxFood;
        }
    }
    public static Player Instance;

    void Start()
    {
        Instance = this;
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
        if (Input.GetButtonDown("Fire2") && foodCount >= settings.maxFood)
        {
            if (levelUpConfig.HasLevelSettings(currentLevel + 1))
            {
                currentLevel += 1;
                var currentLevelSettings = levelUpConfig.GetLevelSettings(currentLevel);
                SetSettings(currentLevelSettings);
            }

            ScoreCount.score += foodCount;
            foodCount = 0;
            ScoreCount.foodScore = 0;
        }

        if (Input.GetButton("Fire1") && foodCount >= 1)
        {
            shooting.PlayerShoot(.2f);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        GameObject collidedWith = coll.gameObject;
        if (collidedWith.CompareTag("Food") && foodCount < settings.maxFood)
        {
            AddFood(Specials.foodMultiplier);
            FoodSpawn.Instance.DestroyFood(collidedWith);
        }
    }

    public void SetSettings(EntitySettings newSettings)
    {
        settings = newSettings;
        transform.localScale = settings.scale;
        playerRenderer.color = settings.playerColor;
        movementOffset = settings.newMovementOffset;
        movement.SetSpeed(settings.speed);
    }

    public void AddFood(int multiplier)
    {
        if (foodCount < settings.maxFood)
        {
            ScoreCount.foodScore += 1 * multiplier;
            foodCount += 1 * multiplier;
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
