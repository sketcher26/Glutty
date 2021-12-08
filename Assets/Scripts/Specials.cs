using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Specials : MonoBehaviour
{
    [SerializeField] private float maxMultiplireCount;
    [SerializeField] private Image multiplierFiller;
    [SerializeField] private Image blinkCooldownFiller;
    [SerializeField] private float multiplierDuration;
    [SerializeField] private float blinkCoolDownSeconds;
    [SerializeField] private int blinkCost;
    [SerializeField] private Player player;
    private float multiplireCount = 0;
    public static int foodMultiplier = 1;
    private bool isBlinking = false;

    void Start()
    {
        blinkCost *= (player.currentLevel + 1);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire3") && multiplireCount >= maxMultiplireCount)
        {
            StartCoroutine(TimedFoodMultiplier(multiplierDuration));
            multiplierFiller.fillAmount = multiplireCount / maxMultiplireCount;
        }

        if (Input.GetKeyDown("space") && !isBlinking && Player.foodCount >= blinkCost)
        {
            StartCoroutine(Blink(blinkCoolDownSeconds));
            StartCoroutine(BlinkCooldown(blinkCoolDownSeconds));
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

    IEnumerator BlinkCooldown(float blinkCoolDown)
    {
        var nextBlink = blinkCoolDown * (player.currentLevel + 1);
        float actualTime = nextBlink;
        while (isBlinking)
        {
            actualTime -= Time.deltaTime;
            blinkCooldownFiller.fillAmount = actualTime / nextBlink;
            yield return null;
        }
    }
    public IEnumerator Blink(float blinkCoolDown)
    {
        isBlinking = true;

        Player.foodCount -= blinkCost;
        var x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        var y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

        if (x >= player.xMax)
            x = player.xMax;
        if (x <= -player.xMax)
            x = -player.xMax;
        if (y >= player.yMax)
            y = player.yMax;
        if (y <= -player.yMax)
            y = -player.yMax;

        transform.position = new Vector3(x, y, 0);

        yield return new WaitForSeconds(blinkCoolDown * (player.currentLevel + 1));
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
