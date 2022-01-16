using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Movement movement;
    [SerializeField] private Shooting shooting;
    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private float foodCollectRadius = 15f;
    [SerializeField] private float arrivalDistance = 0.5f;
    [SerializeField] private float movementOffset = 5f;
    [SerializeField] private EntitySettings settings;
    [SerializeField] private SpriteRenderer enemyRenderer;
    [SerializeField] private LevelUpConfig levelUpConfig;
    [SerializeField] public int currentLevel;
    [SerializeField] public int foodCount = 0;
    [SerializeField] private int spawnInterval = 3;
    [SerializeField] private int foodDropRadius = 5;
    [SerializeField] private int enemySpawnRadius = 5;
    private bool isSpawning = false;
    private bool targetAcquired;
    private Vector3 targetPos;
    private GameObject player;
    public int hitPoints;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        SetSettings(levelUpConfig.GetLevelSettings(currentLevel));
    }

    void FixedUpdate()
    {
        Vector3 worldTargetPos = GetTargetPos();

        Vector2 direction = (worldTargetPos - transform.position).normalized;
        movement.MoveInDirection(direction);

        bool canShootAtPlayer = foodCount >= 1 && Vector2.Distance(player.transform.position, transform.position) <= shooting.ShootingRange;
        CheckClosestFood();

        Vector2 rotateTowardsTarget = worldTargetPos;

        if (canShootAtPlayer)
        {
            rotateTowardsTarget = player.transform.position;
            shooting.EnemyShoot(1f);
        }

        movement.RotateTowardsTarget(rotateTowardsTarget);
        CheckTargetReached(worldTargetPos);
    }

    public void DropFood()
    {
        for (int i = 0; i < foodCount; i++)
            Instantiate(foodPrefab, RandomPointHelper.GetRandomPointAroundGameObject(transform, foodDropRadius), Quaternion.identity);
    }

    public void SetSettings(EntitySettings newSettings)
    {
        settings = newSettings;
        transform.localScale = settings.scale;
        enemyRenderer.color = settings.playerColor;
        movement.SetSpeed(settings.speed);
        hitPoints = settings.hitPoints;
    }

    private void CheckTargetReached(Vector3 targetPos)
    {
        if (Vector2.Distance(transform.position, targetPos) < arrivalDistance)
        {
            targetAcquired = false;
        }
    }

    private void CheckClosestFood()
    {
        var food = FoodSpawn.Instance.Food;
        float minDistanceToFood = float.MaxValue;
        GameObject closestFood = null;

        foreach (GameObject piece in food)
        {
            var distanceToFoodPiece = Vector2.Distance(transform.position, piece.transform.position);

            if (distanceToFoodPiece < foodCollectRadius && distanceToFoodPiece < minDistanceToFood)
            {
                minDistanceToFood = distanceToFoodPiece;
                closestFood = piece;
            }
        }


        if (closestFood != null)
        {
            targetPos = closestFood.transform.position;
            targetAcquired = true;
        }
    }

    public IEnumerator SpawnEnemies(int waitForSeconds)
    {
        isSpawning = true;
        while (true)
        {
            yield return new WaitForSeconds(waitForSeconds);
            var spawnPoint = RandomPointHelper.GetRandomPointAroundGameObject(transform, enemySpawnRadius);
            EnemySpawn.Instance.SpawnEnemy(spawnPoint);
        }
    }

    private Vector3 GetTargetPos()
    {
        if (targetAcquired)
            return targetPos;

        targetPos = RandomPointHelper.GetRandomPointInCameraBounds(movementOffset);
        targetAcquired = true;
        return targetPos;
    }

    public void AddFood()
    {
        foodCount += 1;

        if (foodCount >= settings.maxFood && levelUpConfig.HasLevelSettings(currentLevel + 1))
        {
            currentLevel += 1;
            var currentLevelSettings = levelUpConfig.GetLevelSettings(currentLevel);
            SetSettings(currentLevelSettings);
        }

        if (currentLevel >= 2 && !isSpawning)
        {
            StartCoroutine(SpawnEnemies(spawnInterval));
        }
    }

    public void RemoveFood()
    {
        foodCount -= 1;
    }
}
