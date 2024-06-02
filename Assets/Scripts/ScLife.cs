using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScLife : MonoBehaviour
{
    public ScLifeSlider scLifeSliderSc;

    private Renderer renderer;

    public float life;
    public float maxLife;
    
    // Start is called before the first frame update
    void Start()
    {
        life = maxLife;
        renderer = GetComponent<Renderer>();
    }

    public void TakeDamage (float damage = 5f)
    {
        
        life -= damage;

        if (scLifeSliderSc != null)
        {
            scLifeSliderSc.ChangeActLife(life);
        }
        else
        {
            Debug.LogWarning("ScLifeSlider component not assigned.");
        }

        if (life <= 0)
        {
            Debug.Log("You are DEAD.");
            SceneManager.LoadScene(2);
        }
    }
}
