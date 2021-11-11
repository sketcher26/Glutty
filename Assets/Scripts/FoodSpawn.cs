using System.Collections.Generic;
using UnityEngine;

public class FoodSpawn : MonoBehaviour
{
    [SerializeField] GameObject spawnObject;
    [SerializeField] float initialDelay;
    [SerializeField] float repeatDelay;
    [SerializeField] float maxSpawnCount = 3f;
    [SerializeField] float offset = 5f;
    private List<GameObject> food;
    public List<GameObject> Food => food;
    public GameObject SpawnObject => spawnObject;

    public static FoodSpawn Instance; // входная точка

    void Awake()
    {
        food = new List<GameObject>();
        Instance = this;
    }

    void Start()
    {
        InvokeRepeating("SpawnFood", initialDelay, repeatDelay);
    }

    void SpawnFood()
    {
        if (food.Count < maxSpawnCount)
        {
            Vector2 spawnPoint = RandomPointHelper.GetRandomPointInCameraBounds(offset);
            GameObject foodPiece = Instantiate(spawnObject, spawnPoint, Quaternion.identity);
            food.Add(foodPiece);
        }
    }

    public void DestroyFood(GameObject foodPiece)
    {
        food.Remove(foodPiece);
        Destroy(foodPiece);
    }
}
