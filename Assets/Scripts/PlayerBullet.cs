using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    void OnTriggerEnter2D(Collider2D coll)
    {
        GameObject collidedWith = coll.gameObject;
        if(collidedWith.CompareTag("Enemy"))
        {
        Destroy(collidedWith);
        Destroy(gameObject);
        }
    }    
}
