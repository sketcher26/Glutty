using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour
{
    public static float foodScore;
    public static int score = 0;
    [SerializeField] Image filler;
    [SerializeField] Text inGameScore;
    [SerializeField] Text endGameScore;
    [SerializeField] TMP_Text food;

    void Update()
    {
        foodScore = Player.foodCount;
        food.text = foodScore.ToString();
        inGameScore.text = score.ToString();
        endGameScore.text = "Твой Счет:" + score.ToString();
        filler.fillAmount = foodScore / Player.Instance.MaxFood;
    }
}
