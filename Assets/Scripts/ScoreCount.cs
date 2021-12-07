using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour
{
    public static float foodScore = 0;
    public static int score = 0;
    [SerializeField] Image filler;
    [SerializeField] Text inGameScore;
    [SerializeField] Text endGameScore;
    TMP_Text food;
    void Start()
    {
        food = GetComponent<TMP_Text>();
    }

    void Update()
    {
        food.text = foodScore.ToString();
        inGameScore.text = score.ToString();
        endGameScore.text = "Твой Счет:" + score.ToString();
        filler.fillAmount = foodScore / Player.Instance.MaxFood;
    }
}
