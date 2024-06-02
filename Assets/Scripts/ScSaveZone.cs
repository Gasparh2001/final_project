using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScSaveZone : MonoBehaviour
{
    public TimeControler timeControler;

    private bool youAreWin = false;
    
    public float stopTheChrono;

    // Start is called before the first frame update
    void Start()
    {
        if (timeControler == null)
        {
            timeControler = GetComponent<TimeControler>();

            if (timeControler == null)
            {
                Debug.LogError("Sc Timecontroler not found");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D enter)
    {
        youAreWin = true;
        YouAreWin();
        if (timeControler != null)
        {
            stopTheChrono = timeControler.chronoTimeF;
            Debug.Log("STOP the CHRONO" + stopTheChrono);
            SaveManager.SavePlayerData(this);
            Debug.Log("The Archive are Saved");
        }
        else
        {
            Debug.LogError("timeControler, Not Assigned");
        }
    }
    public void YouAreWin()
    {
        if (youAreWin == true)
        {
            Debug.Log("You WIN the GAME.");
            TimeControler.Instance.FinalTime();
            SceneManager.LoadScene(3);
            SaveManager.SavePlayerData(this);
        }
    }
}
