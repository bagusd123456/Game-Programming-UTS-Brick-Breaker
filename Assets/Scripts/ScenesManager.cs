using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public GameObject player = null;
    public GameObject LevelFinished = null;
    public GameObject pressAnyKey = null;
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
            gamePause = true;
            pressAnyKey.SetActive(true);
            Time.timeScale = 0;
        }

        if (gamePause == true && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Time.timeScale = 1;
            pressAnyKey.SetActive(false);
            gamePause = false;
        }
    }
}
