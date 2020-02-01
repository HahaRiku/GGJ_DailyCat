using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<int> PlayerPoints = new List<int>();
    //[SerializeField] private float MaxTime;
    [SerializeField] private Text uitext;
    [SerializeField] private float mainTimer;
    public GameObject Player1;
    public GameObject Player2;



    private float timer;
    private bool canCount = true;
    private bool doOnce = false;
    private bool EndGame = false;
   
    // Start is called before the first frame update
    void Start()
    {
        timer = mainTimer;
    }

    // Update is called once per frame
    void Update()
    {
        Timer();

    }

    private void Timer()
    {
        if (timer >= 0.0f && canCount)
        {
            timer -= Time.deltaTime;
            uitext.text = timer.ToString("F");

        }
        else if (timer <= 0.0f && !doOnce)
        {
            canCount = false;
            doOnce = true;
            uitext.text = "0.0";
            timer = 0.0f;
            EndGame = true;
        }
    }


}
