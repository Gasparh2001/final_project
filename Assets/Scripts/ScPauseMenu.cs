using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScPauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuPause;
    [SerializeField] private GameObject pauseButton;
    
    private bool isPaused = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Pause()
    {
        Debug.Log("GAME is paused");
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
        menuPause.SetActive(true);
        isPaused = true;
    }
    public void Resume()
    {
        Debug.Log("Resume the GAME");
        Time.timeScale = 1f;
        pauseButton.SetActive(true);
        menuPause.SetActive(false);
        isPaused = false;
    }
    public void Restart()
    {
        Debug.Log("Restart the GAME");
        Time.timeScale = 1f;
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        isPaused = false;
    }
    public void MenuBtn()
    {
        Debug.Log("Go to GAME menu");
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        isPaused = false;
    }
}
