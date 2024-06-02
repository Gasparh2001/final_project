using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeControler : MonoBehaviour
{
    public static TimeControler Instance;
    [SerializeField] TextMeshProUGUI time;
    public float chronoTimeF;
    private float chronoTime = 180;

    private void Awake()
    {
        if (TimeControler.Instance == null )
        {
            TimeControler.Instance = this;
            DontDestroyOnLoad( this.gameObject );
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        chronoTime -= Time.deltaTime;
        time.text = "" + chronoTime.ToString("f1");
        if(chronoTime < 0)
        {
            Debug.Log("You are DEAD.");
            SceneManager.LoadScene(2);
        } 
    }
    public void FinalTime()
    {
        chronoTimeF = chronoTime;
    }

    
}
