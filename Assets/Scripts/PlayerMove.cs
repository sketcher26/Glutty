using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private float speed = 2f;
    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(x,y);
        Vector2 move = (Vector2)direction * speed * Time.fixedDeltaTime;

        Vector2 nextPosition = playerRB.position + move;
        playerRB.MovePosition(nextPosition);
    }
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
