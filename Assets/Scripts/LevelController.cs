using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] GameObject spawnObject;
    [SerializeField] float initialDelay;
    [SerializeField] float repeatDelay;
    [SerializeField] float spawnCount = 3f;
    private GameObject[] getCount;

    
    void Start()
    {
        InvokeRepeating("ObjectSpawn", initialDelay, repeatDelay);
    }
    void Update()
    {
        getCount = GameObject.FindGameObjectsWithTag("Food");        
    }
    
    void ObjectSpawn()
    {        
        if (getCount.Length < spawnCount)
        {
        float screenX = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
        float screenY = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);

        Vector2 spawnPoint = new Vector2(screenX, screenY);
        Instantiate(spawnObject, spawnPoint, Quaternion.identity);
        }
    }
}
