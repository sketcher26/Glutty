using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float attackRange = 20f;
    [SerializeField] public Transform shootingPoint;
    [SerializeField] private GameObject bulletPrefab;
    private float foodCount = 0f;
    private GameObject player;
    private GameObject bulletInst;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (foodCount >= 1f && Vector2.Distance(player.transform.position, transform.position) <= attackRange)
        {
            Vector2 lookDirection = player.transform.position - transform.position;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            Invoke("Shoot", 1);
        }

    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        GameObject collidedWith = coll.gameObject;
        if (collidedWith.CompareTag("Food"))
        {
            foodCount += 1f;
            Destroy(collidedWith);
        }
    }

    void Shoot()
    {
        if (bulletInst == null)
        {
            bulletInst = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
            foodCount -= 1f;
        }
    }
}
