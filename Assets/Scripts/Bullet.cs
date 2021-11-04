using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float speed = 10f;
    public Rigidbody2D rb;

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

    /* void OnTriggerEnter2D(Collider2D coll)
    {
        GameObject collidedWith = coll.gameObject;
        if (collidedWith.CompareTag("Player") && player.FoodCount >= 1)
        {
            player.FoodCount -= 1;
            Destroy(gameObject);
        }
        else if (collidedWith.CompareTag("Player") && player.FoodCount <= 0)
        {
            Destroy(collidedWith);
            Destroy(gameObject);
        }
        else if (collidedWith.CompareTag("Enemy"))
        {
            Destroy(collidedWith);
            Destroy(gameObject); 
        }
    } */
}
