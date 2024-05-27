using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScLife : MonoBehaviour
{
    [SerializeField] private float life;

    [SerializeField] private float maxLife;

    [SerializeField] private ScLifeSlider lifeSlider;

    // Start is called before the first frame update
    void Start()
    {
        life = maxLife;
        lifeSlider.StartLifeSlider(life);
    }

    public void TakeDamage (float damage)
    {
        life -= damage;
        lifeSlider.ChangeActLife(life);

        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeHeal (float heal)
    {
        if ((life + heal) > maxLife)
        {
            life = maxLife;
        }

        else
        {
            life += heal;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
