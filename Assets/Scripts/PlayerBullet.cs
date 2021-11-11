using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    protected override void OnTriggerEnter2D(Collider2D coll)
    {
        GameObject collidedWith = coll.gameObject;
        if (collidedWith.CompareTag("Enemy"))
        {
            collidedWith.GetComponent<Enemy>().DropFood();
            Destroy(collidedWith);
            Destroy(gameObject);
        }
    }
}
