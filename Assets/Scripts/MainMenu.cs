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
        highscore.text = "Хайскор:" + PlayerPrefs.GetInt("highscore");
        difficultySlider.value = PlayerPrefs.GetFloat("difficulty");
        difficultySlider.onValueChanged.AddListener((v) =>
        {
            PlayerPrefs.SetFloat("difficulty", v);
            /* if(v == 0)
            difficultyText.text = "Лох";

            else if(v == 1)
            difficultyText.text = "Не лох";

            else if(v == 2)
            difficultyText.text = "Ваще не лох";

            else if(v == 3)
            difficultyText.text = "Игра лох"; */
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

}
