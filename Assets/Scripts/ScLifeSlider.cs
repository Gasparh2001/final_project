using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScLifeSlider : MonoBehaviour
{
    private Slider slider;

    // Start is called before the first frame update
    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void ChangeMaxLife (float maxLife)
    {
        slider.maxValue = maxLife;
    }

    public void ChangeActLife (float lifeValue)
    {
        slider.value = lifeValue;
    }

    public void StartLifeSlider ( float lifeValue)
    {
        ChangeMaxLife(lifeValue);
        ChangeActLife(lifeValue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
