using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool gameOver;
    public GameObject gameOverPanel;
    public GameObject pausePanel;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        StartCoroutine(ExitRoutine());
    }
    private IEnumerator ExitRoutine()
    {
        yield return new WaitForSeconds(2);
        Exit();
    }
    public void Exit()
    {
        if(gameOver)
        {
            gameOver = false;
        }
        SceneManager.LoadScene("MainMenu");
    }
    public void Restart()
    {
        TogglePause();
        SceneManager.LoadScene("Game");
    }
    public void TogglePause()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
    }
}
