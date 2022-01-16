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

                FindObjectOfType<AudioManager>().Play(SoundType.Collision);

                Destroy(gameObject);

            }
            else if (enemy.hitPoints <= damage)
            {
                collidedWith.GetComponent<Enemy>().DropFood();

                FindObjectOfType<AudioManager>().Play(SoundType.Collision);

                ScoreCount.score += enemy.currentLevel + 1;
                
                Destroy(collidedWith);
                Destroy(gameObject);
            }
        }
    }
}
