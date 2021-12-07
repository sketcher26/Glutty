using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    [SerializeField] private int damage;
    protected override void OnTriggerEnter2D(Collider2D coll)
    {
        GameObject collidedWith = coll.gameObject;
        if (collidedWith.CompareTag("Player") && Player.foodCount >= damage)
        {
            Player.foodCount -= damage;
            ScoreCount.foodScore -= damage;
            Destroy(gameObject);
        }
        else if (collidedWith.CompareTag("Player") && Player.foodCount < damage)
        {
            Destroy(collidedWith);
            Destroy(gameObject);
        }
    }
}
