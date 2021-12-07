using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartMenu : MonoBehaviour
{
    [SerializeField] private GameObject restart;
    [SerializeField] private GameObject player;

    private void FixedUpdate()
    {
        if (player == null)
        {
            Time.timeScale = 0;
            restart.SetActive(true);
        }
    }

    public void RestartGame()
    {
        if (ScoreCount.score > PlayerPrefs.GetInt("highscore"))
            PlayerPrefs.SetInt("highscore", ScoreCount.score);

        ScoreCount.score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        if (ScoreCount.score > PlayerPrefs.GetInt("highscore"))
            PlayerPrefs.SetInt("highscore", ScoreCount.score);

        ScoreCount.score = 0;
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
