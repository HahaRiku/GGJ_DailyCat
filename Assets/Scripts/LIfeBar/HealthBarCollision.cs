using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarCollision : MonoBehaviour
{
    [SerializeField] HealthBar healthBar = null;
    // Start is called before the first frame update

    [SerializeField] float StartHealth = 0;
    [SerializeField] float MaxHealth = 100 ;
    [SerializeField] float lifeHeal = 8f;

    LifeBarSystem lifeBarSystem;
    void Start()
    {
        setHealth();
    }

    private void setHealth()
    {
        //LifeBarSystem lifeBarSystem = new LifeBarSystem(this.health);
        lifeBarSystem = new LifeBarSystem(MaxHealth,StartHealth);
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
            //healthBar.getLifeBarSystem().Damage((float)lifeDamage * Time.deltaTime);
            healthBar.getLifeBarSystem().Heal((float)lifeHeal * Time.deltaTime);
        }
                  
    }
}
