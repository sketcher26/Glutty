using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Slider difficultySlider;
    [SerializeField] private TMP_Text difficultyText;
    public TMP_Text highscore;

    void Start()
    {
        if(PlayerPrefs.GetFloat("difficulty") == 0)
        PlayerPrefs.SetFloat("difficulty",1);
        
        SetDifficulty(PlayerPrefs.GetFloat("difficulty"));
        difficultySlider.value = PlayerPrefs.GetFloat("difficulty");
        highscore.text = "Хайскор:" + PlayerPrefs.GetInt("highscore");
        difficultySlider.onValueChanged.AddListener((v) =>
        {
            SetDifficulty(v);
            PlayerPrefs.SetFloat("difficulty", v);
        });
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    void SetDifficulty(float v)
    {
        if (v == 1)
            difficultyText.text = "Лох";

        else if (v == 2)
            difficultyText.text = "Не лох";

        else if (v == 3)
            difficultyText.text = "Ваще не лох";

        else if (v == 4)
            difficultyText.text = "Игра лох";
    }

}
