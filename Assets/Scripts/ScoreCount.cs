using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour
{
    public static int foodScore = 0;
    public static int score = 0;
    [SerializeField] Text overallScore;
    TMP_Text food;
    void Start()
    {
        food = GetComponent<TMP_Text>();
    }

    void Update()
    {
        food.text = foodScore.ToString();
        overallScore.text = score.ToString();
    }
}
