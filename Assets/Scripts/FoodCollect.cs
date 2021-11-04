using UnityEngine;
using UnityEngine.Events;

public class FoodCollect : MonoBehaviour
{
    [SerializeField] UnityEvent foodCollected;
    void OnTriggerEnter2D(Collider2D coll)
    {
        GameObject collidedWith = coll.gameObject;
        if (collidedWith.CompareTag("Food"))
        {
            foodCollected.Invoke();
            FoodSpawn.Instance.DestroyFood(collidedWith);
        }
    }
}
