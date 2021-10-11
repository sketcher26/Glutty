using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    [SerializeField] GameObject spawnObject;
    [SerializeField] private GameObject destination;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float offset = 5f;

    void Update()
    {
        ObjectSpawn();
        Move();
    }

    private void Move()
    {
        if (transform.position != destination.transform.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination.transform.position, speed * Time.deltaTime);
        }
        else
        {
            Destroy(destination);
        }
    }

    void ObjectSpawn()
    {
        if (destination == null)
        {
            var min = Camera.main.ScreenToWorldPoint(Vector2.zero);
            var xMax = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0));
            var yMax = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height));
            float screenX = Random.Range(min.x + offset, xMax.x - offset);
            float screenY = Random.Range(min.y + offset, yMax.y - offset);

            Vector2 spawnPoint = new Vector2(screenX, screenY);
            destination = Instantiate(spawnObject, spawnPoint, Quaternion.identity);
        }
    }
}
