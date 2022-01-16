using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject spawnObject;
    [SerializeField] private float initialDelay;
    [SerializeField] private float repeatDelay;
    [SerializeField] private float spawnCount = 3;
    [SerializeField] private float offset = 10f;
    private GameObject[] getCount;
    public static EnemySpawn Instance;

    void Awake()
    {
        spawnCount = PlayerPrefs.GetFloat("difficulty");
        Instance = this;
    }

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemyInRandomScreenPoint), initialDelay, repeatDelay);
    }
    void Update()
    {
        getCount = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void SpawnEnemyInRandomScreenPoint()
    {
        if (getCount.Length < spawnCount)
        {
            var xMin = Camera.main.ScreenToWorldPoint(Vector2.zero);
            var yMin = Camera.main.ScreenToWorldPoint(Vector2.zero);
            var xMax = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0));
            var yMax = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height));

            var spawnDirection = Random.Range(0, 2); //выбор направления спавна(0 = вправо/влево, 1 = вверх/вниз)
            var spawnSide = Random.Range(0, 2); //выбор стороны спавна(0 = сверху/справа, 1 = снизу/слева)
            var spawnPoint = Vector2.zero;
            if (spawnDirection == 0)
            {
                spawnPoint.x = Random.Range(xMin.x, xMax.x);
                if (spawnSide == 0)
                    spawnPoint.y = yMax.y + offset;
                else
                    spawnPoint.y = yMin.y - offset;
            }
            else
            {
                spawnPoint.y = Random.Range(yMin.y, yMax.y);
                if (spawnSide == 0)
                    spawnPoint.x = xMax.x + offset;
                else
                    spawnPoint.x = xMin.x - offset;
            }

            Vector2 spawnPlace = new Vector2(spawnPoint.x, spawnPoint.y);
            SpawnEnemy(spawnPlace);
        }
    }

    public void SpawnEnemy(Vector2 spawnPosition)
    {
        Instantiate(spawnObject, spawnPosition, Quaternion.identity);
    }
}
