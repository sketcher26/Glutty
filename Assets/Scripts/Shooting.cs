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

    public void ShootWithDelay()
    {
        if (canShoot && bulletInst == null)
        {
            StartCoroutine(PrepareAndShoot());
        }
    }

    public void Shoot()
    {
        bulletInst = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
        shot.Invoke();
    }

    private IEnumerator PrepareAndShoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(1);
        bulletInst = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
        shot.Invoke();
        canShoot = true;
    }
}
