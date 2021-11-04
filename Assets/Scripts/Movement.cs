using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public float speed = 2f;
    
    [HideInInspector]public float xMax;
    [HideInInspector]public float yMax;
    public Rigidbody2D Rb => rb;

    void Start()
    {
        yMax = Camera.main.orthographicSize;
        xMax = yMax * Screen.width / Screen.height;
        
    }
    
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void RotateTowardsTarget(Vector3 target)
    {
        Vector2 lookDirection = target - transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void MoveInDirection(Vector2 direction)
    {
        Vector2 move = direction.normalized * speed * Time.fixedDeltaTime;

        Vector2 nextPosition = rb.position + move;
        rb.MovePosition(nextPosition);
    }
}
