using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour
{
    public static int foodScore = 0;
    TMP_Text food;
    void Start()
    {
        food = GetComponent<TMP_Text>();
    }

    void Update()
    {
        food.text = foodScore.ToString();
    }
}
