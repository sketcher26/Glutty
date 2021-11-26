using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartMenu : MonoBehaviour
{
    [SerializeField] private GameObject restart;
    [SerializeField] private GameObject player;
    private void Awake()
    {
        restart.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (player == null)
        {
            restart.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
