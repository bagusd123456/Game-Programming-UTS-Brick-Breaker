using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public GameObject player = null;
    public GameObject LevelFinished = null;
    public GameObject pressAnyKey = null;
    public GameObject gameOver = null;

    BallController ballController;

    private bool gamePause;

    private void Start()
    {
        if(player != null)
        {
            ballController = player.GetComponent<BallController>();
        }
        Time.timeScale = 1;
    }

    private void Update()
    {
        PauseMenu();
        CheckObstacles();
        CheckLives();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level 2");
        gamePause = true;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CheckObstacles()
    {
        if(LevelFinished != null)
        {
            if (ballController.obstacleList.Length < 1)
            {
                LevelFinished.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public void PauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gamePause)
        {
            if(pressAnyKey != null)
            {
                gamePause = true;
                pressAnyKey.SetActive(true);
                Time.timeScale = 0;
            }
        }

        if (gamePause == true && Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1;
            pressAnyKey.SetActive(false);
            gamePause = false;
        }
    }

    public void CheckLives()
    {
        if(ballController != null)
        {
            if(ballController.Lives <= 0 && gameOver != null)
            {
                Time.timeScale = 0;
                gameOver.SetActive(true);
            }
        }
    }
}
