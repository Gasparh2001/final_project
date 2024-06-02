using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScWin : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeScore;

    private float timeScoreFl;

    // Update is called once per frame
    void Update()
    {
        PlayerData playerData = SaveManager.LoadPlayerData();
        timeScore.text = "" + playerData.stopTheChrono.ToString("f1");
        timeScoreFl = playerData.stopTheChrono;
    }
    public void MenuBtnWin()
    {
        Debug.Log("Go to GAME menu");
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
