using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Shooting : MonoBehaviour
{
    [SerializeField] private float attackRange = 20f;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private UnityEvent shot;
    private bool canShoot = true;
    private GameObject bulletInst;

    public float ShootingRange => attackRange;

    public void EnemyShoot(float shootingDelay)
    {
        if (canShoot && bulletInst == null)
        {
            StartCoroutine(PrepareAndShoot(shootingDelay));
        }
    }

    public void PlayerShoot(float shootingDelay)
    {
        if (canShoot)
        {
            StartCoroutine(ShootAndPrepare(shootingDelay));
        }
    }

    private IEnumerator PrepareAndShoot(float shootingDelay)
    {
        canShoot = false;
        shot.Invoke();
        yield return new WaitForSeconds(shootingDelay);
        bulletInst = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
        canShoot = true;
    }
    private IEnumerator ShootAndPrepare(float shootingDelay)
    {
        canShoot = false;
        shot.Invoke();
        bulletInst = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
        yield return new WaitForSeconds(shootingDelay);
        canShoot = true;
    }
}
