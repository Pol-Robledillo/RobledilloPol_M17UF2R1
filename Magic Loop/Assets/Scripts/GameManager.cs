using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public AudioClip[] sounds;
    private AudioSource audioSource;
    public bool gameOver;
    public int enemiesInFloor, defeatedEnemies;
    public GameObject gameOverPanel;
    public GameObject gameWonPanel;
    public GameObject pausePanel;
    public GameObject endPortal;
    public GameObject guideArrow;
    public event Action OpenPortalEvent = delegate { };
    public delegate void OpenPortal();
    public event OpenPortal openPortal;
    public void EndAvailable()
    {
        openPortal.Invoke();
        OpenPortalEvent.Invoke();
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameOver = false;
        defeatedEnemies = 0;
    }
    public void GameOver()
    {
        if (gameOver)
        {
            gameOverPanel.SetActive(true);
            audioSource.clip = sounds[0];
            audioSource.Play();
        }
        else
        {
            gameWonPanel.SetActive(true);
            audioSource.clip = sounds[1];
            audioSource.Play();
        }
        StartCoroutine(ExitRoutine());
    }
    private IEnumerator ExitRoutine()
    {
        yield return new WaitForSeconds(8);
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
    public void PrepareEndGame()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        enemiesInFloor = enemies.Length;

        endPortal = GameObject.Find("Portal");
        endPortal.SetActive(false);
    }
}
