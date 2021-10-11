using UnityEngine;

public class FoodSpawn : MonoBehaviour
{
    [SerializeField] GameObject spawnObject;
    [SerializeField] float initialDelay;
    [SerializeField] float repeatDelay;
    [SerializeField] float spawnCount = 3f;
    [SerializeField] float offset = 5f;
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
            var min = Camera.main.ScreenToWorldPoint(Vector2.zero);
            var xMax = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0));
            var yMax = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height));
            float screenX = Random.Range(min.x + offset, xMax.x - offset);
            float screenY = Random.Range(min.y + offset, yMax.y - offset);

            Vector2 spawnPoint = new Vector2(screenX, screenY);
            Instantiate(spawnObject, spawnPoint, Quaternion.identity);
        }
    }
}
