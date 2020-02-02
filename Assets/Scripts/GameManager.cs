using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<int> PlayerPoints = new List<int>();
    //[SerializeField] private float MaxTime;
    [SerializeField] private Text uitext;
    [SerializeField] private Image uiimage1;
    [SerializeField] private Image uiimage2;
    [SerializeField] private float mainTimer;
    public GameObject Player1;
    public GameObject Player2;
    public GameObject EndUI;
    public Sprite[] numbers;

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
        Restart();

    }

    private void Timer()
    {
        if (timer >= 0.0f && canCount)
        {
            timer -= Time.deltaTime;
            uitext.text = timer.ToString("F");
            uiimage1.sprite = numbers[(int)timer%10];
            uiimage2.sprite = numbers[(int)timer/10];


        }
        else if (timer <= 0.0f && !doOnce)
        {
            canCount = false;
            doOnce = true;
            uitext.text = "0.0";
            timer = 0.0f;
            EndUI.SetActive(true);
            Time.timeScale = 0f;
            EndGame = true;
            
        }
    }
    private void Restart()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            canCount = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    


}
