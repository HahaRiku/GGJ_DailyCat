using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private LifeBarSystem lifeBarSystem;
    public void Setup(LifeBarSystem lifeBarSystem)
    {
        this.lifeBarSystem = lifeBarSystem ;
    }

    public LifeBarSystem getLifeBarSystem()
    {
        return lifeBarSystem;
    }

    // Update is called once per frame
    void Update()
    {
        SetLifeBar();
    }
    private void SetLifeBar()
    {
        if(transform.Find("Bar") != null)
        {
            //Debug.Log(lifeBarSystem.GetHealthPercent());
            transform.Find("Bar").localScale = 
                new Vector3(lifeBarSystem.GetHealthPercent(), 1);
        }
    }
}
