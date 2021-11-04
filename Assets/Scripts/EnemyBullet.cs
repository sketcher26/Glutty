using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    protected override void OnTriggerEnter2D(Collider2D coll)
    {
        GameObject collidedWith = coll.gameObject;
        if (collidedWith.CompareTag("Player") && Player.foodCount >= 1)
        {
            Player.foodCount -= 1;
            ScoreCount.foodScore -= 1;
            Destroy(gameObject);
        }
        else if (collidedWith.CompareTag("Player") && Player.foodCount <= 0)
        {
            Destroy(collidedWith);
            Destroy(gameObject);
        }
    }
}
