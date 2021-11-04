using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Movement movement;
    [SerializeField] private Shooting shooting;
    [SerializeField] private float foodCollectRadius = 15f;
    [SerializeField] private float arriveDistance = 0.5f;
    [SerializeField] private float movementOffset = 5f;
    private bool targetAcquired;
    private Vector3 targetPos;

    private int foodCount = 0;
    private GameObject player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
            shooting.ShootWithDelay();
        }

        movement.RotateTowardsTarget(rotateTowardsTarget);
        CheckTargetReached(worldTargetPos);
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

    private Vector3 GetTargetPos()
    {
        if (targetAcquired)
            return targetPos;

        targetPos = CameraPositionHelper.GetRandomPointInCameraBounds(movementOffset);
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
