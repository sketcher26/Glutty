using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Movement movement;
    [SerializeField] private Shooting shooting;
    [SerializeField] private float foodCollectRadius = 15f;
    [SerializeField] private float arriveDistance = 0.5f;
    [SerializeField] private float movementOffset = 5f;
    [SerializeField] private PlayerSettings settings;
    [SerializeField] private SpriteRenderer enemyRenderer;
    [SerializeField] private LevelUpConfig levelUpConfig;
    [SerializeField] private FoodSpawn foodSpawn;
    [SerializeField] private int currentLevel;
    [SerializeField] private int foodCount = 0;
    private bool canSpawn = true;
    private bool isQuitting;
    private bool targetAcquired;
    private Vector3 targetPos;
    private GameObject player;

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
        if (foodCount >= settings.maxFood && levelUpConfig.HasLevelSettings(currentLevel + 1))
        {
            currentLevel += 1;
            var currentLevelSettings = levelUpConfig.GetLevelSettings(currentLevel);
            SetSettings(currentLevelSettings);
        }

        if (currentLevel >= 2 && canSpawn)
        {
            StartCoroutine(SpawnEnemies(3));
        }

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

    void OnApplicationQuit()
    {
        isQuitting = true;
    }
    void OnDestroy()
    {
        int[] food = new int[foodCount];

        if (!isQuitting)
            foreach (int foo in food)
                Instantiate(FoodSpawn.Instance.SpawnObject, RandomPointHelper.GetRandomPointInGameObject(transform, 5), Quaternion.identity);
    }

    public void SetSettings(PlayerSettings newSettings)
    {
        settings = newSettings;
        transform.localScale = settings.scale;
        enemyRenderer.color = settings.playerColor;
        movementOffset = settings.newMovementOffset;
        movement.SetSpeed(settings.speed);
    }

    private void CheckTargetReached(Vector3 targetPos)
    {
        if (Vector2.Distance(transform.position, targetPos) < arriveDistance)
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
        canSpawn = false;
        yield return new WaitForSeconds(waitForSeconds);
        Instantiate(EnemySpawn.Instance.spawnObject, RandomPointHelper.GetRandomPointInGameObject(transform, 5), Quaternion.identity);
        canSpawn = true;
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
    }

    public void RemoveFood()
    {
        foodCount -= 1;
    }
}
