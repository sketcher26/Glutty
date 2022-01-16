using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float speed = 10f;
    public Rigidbody2D rb;

    void Awake()
    {
        FindObjectOfType<AudioManager>().Play(SoundType.Shot);
    }
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void Update()
    {
        var screenBound = new Vector3(Screen.width, Screen.height);
        if (!GetComponent<Renderer>().isVisible)
        {
            Destroy(gameObject);
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D coll)
    {

    }
}
