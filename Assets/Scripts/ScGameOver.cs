using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScGameOver : MonoBehaviour
{
    public void RestartGO()
    {
        Debug.Log("Restart the GAME");
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
    public void MenuGO()
    {
        Debug.Log("Go to the GAME menu");
        SceneManager.LoadScene(0);
    }
}
