using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScLifeSlider : MonoBehaviour
{
    private Slider liveSlider;

    private void Awake()
    {
        liveSlider = GetComponent<Slider>();
        if (liveSlider == null)
        {
            Debug.LogError("Slider component Not Found");
        }
    }

    public void ChangeMaxLife (float maxLife)
    {
        liveSlider.maxValue = maxLife;
    }

    public void ChangeActLife (float lifeValue)
    {
        liveSlider.value = lifeValue;
    }

    public void StartLifeSlider ( float lifeValue)
    {
        ChangeMaxLife(lifeValue);
        ChangeActLife(lifeValue);
    }
}
