using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antique : MonoBehaviour
{
    [SerializeField] int pointsPerObjectFixed = 10;
    [SerializeField] HealthBar healBarControl = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkHealBar();
    }
    private void checkHealBar()
    {
        if(healBarControl.getLifeBarSystem().GetHealthPercent() >= 1)
        {
            if(FindObjectOfType<GameManager>()!= null)
            {
                FindObjectOfType<GameManager>().AddToScore(pointsPerObjectFixed);
                FindObjectOfType<GameManager>().CreateNewOne(transform.position);
            }           
            Destroy(gameObject);
            
        }
    }
    IEnumerator createNewAntique()
    {
        yield return new WaitForSeconds(3);
    }
}
