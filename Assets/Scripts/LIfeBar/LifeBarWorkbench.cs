using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBarWorkbench : MonoBehaviour
{
    [SerializeField] HealthBar healthBar = null;
    // Start is called before the first frame update


    [SerializeField] float health = 100 ;
    [SerializeField] float lifeDamage = 8f;

    LifeBarSystem lifeBarSystem;
    void Start()
    {
        setHealth(health);
    }

    private void setHealth(float health)
    {
        //LifeBarSystem lifeBarSystem = new LifeBarSystem(this.health);
        lifeBarSystem = new LifeBarSystem(health);
        healthBar.Setup(lifeBarSystem);
    }



    // Update is called once per frame
    /*void Update()
    {
        
    }*/

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.Z))
        {
            healthBar.getLifeBarSystem().Damage((float)lifeDamage * Time.deltaTime);
        }
                  
    }
}
