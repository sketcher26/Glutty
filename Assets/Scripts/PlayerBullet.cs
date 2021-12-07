using UnityEngine.Events;
using UnityEngine;

public class PlayerBullet : Bullet
{
    [SerializeField] private int damage;

    protected override void OnTriggerEnter2D(Collider2D coll)
    {
        GameObject collidedWith = coll.gameObject;
        if (collidedWith.CompareTag("Enemy") && collidedWith.TryGetComponent(out Enemy enemy))
        {
            if (enemy.hitPoints > damage)
            {
                enemy.hitPoints -= damage;

                FindObjectOfType<AudioManager>().PlayRandom("Collision 1", "Collision 4");

                Destroy(gameObject);

            }
            else if (enemy.hitPoints <= damage)
            {
                collidedWith.GetComponent<Enemy>().DropFood();

                FindObjectOfType<AudioManager>().PlayRandom("Collision 1", "Collision 4");

                Destroy(collidedWith);
                Destroy(gameObject);
            }
        }

    }
}
