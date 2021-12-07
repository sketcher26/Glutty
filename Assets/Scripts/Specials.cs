using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Specials : MonoBehaviour
{
    [SerializeField] private float maxMultiplireCount;
    [SerializeField] private Image multiplierFiller;
    [SerializeField] private float multiplierDuration;
    [SerializeField] private float blinkCoolDown; 
    [SerializeField] private int blinkCost;
    private float multiplireCount = 0;
    public static int foodMultiplier = 1;
    private bool isBlinking = false;

    void Update()
    {
        if (Input.GetButtonDown("Fire3") && multiplireCount >= maxMultiplireCount)
        {
            StartCoroutine(TimedFoodMultiplier(multiplierDuration));
            multiplierFiller.fillAmount = multiplireCount / maxMultiplireCount;
        }

        if (Input.GetKeyDown("space") && !isBlinking && Player.foodCount >= blinkCost)
        {
            StartCoroutine(Blink(blinkCoolDown));
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        var collidedWith = coll.gameObject;
        if (coll.name == "EnemyDrop(Clone)" && multiplireCount < maxMultiplireCount)
        {
            multiplireCount += 1;
            Destroy(collidedWith);
        }

        multiplierFiller.fillAmount = multiplireCount / maxMultiplireCount;
    }

    public IEnumerator Blink(float blinkCoolDown)
    {
        isBlinking = true;
        Player.foodCount -= blinkCost;
        transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        yield return new WaitForSeconds(blinkCoolDown);
        isBlinking = false;
    }

    public IEnumerator TimedFoodMultiplier(float multiplierDuration)
    {
        foodMultiplier += 1;
        multiplireCount = 0;
        yield return new WaitForSeconds(multiplierDuration);
        foodMultiplier -= 1;
    }

}
