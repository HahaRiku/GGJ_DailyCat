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
    [SerializeField] private Image uiimage3;
    [SerializeField] private Image uiimage4;
    [SerializeField] Text scoreText = null;
    [SerializeField] GameObject[] antique;

    public static GameManager manager;

    [SerializeField] Image emptyobject=null;
    //state variable
    int currentScore = 0;

    [SerializeField] float timeToReCreate = 5f;
    [SerializeField] private float mainTimer;
    public GameObject Player1;
    public GameObject Player2;
    public GameObject EndUI;
    public Sprite[] numbers;

    private float timer;
    private bool canCount = true;
    private bool doOnce = false;
    public bool EndGame = false;

    //private void Awake()
    //{
    //    int gameStatusCount = FindObjectsOfType<AntiqueController>().Length;
    //    if (gameStatusCount > 1)
    //    {
    //        Destroy(gameObject);
    //    }
    //}
    // Start is called before the first frame update
    void Start()
    {
       manager = this;
       timer = mainTimer;
        scoreText.text = currentScore.ToString();
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
            uiimage1.sprite = numbers[(int)timer % 10];
            uiimage2.sprite = numbers[(int)timer / 10];
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
            ScoreMove();

        }
    }


    public void AddToScore(int pointsPerObjectFixed)
    {
        currentScore = currentScore + pointsPerObjectFixed;
        Debug.Log(currentScore);
        scoreText.text = currentScore.ToString();
        uiimage3.sprite = numbers[currentScore / 10];
        uiimage4.sprite = numbers[currentScore % 10];
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public void CreateNewOne(Vector3 position)
    {
        StartCoroutine(ReCreateNewOne(position));
    }
    IEnumerator ReCreateNewOne(Vector3 position)
    {
        int random_num = Random.Range(0, antique.Length);
        yield return new WaitForSeconds(timeToReCreate);
        Instantiate(antique[random_num], position, transform.rotation);
    }


    bool restart=false;
    public void reload()
    {
        restart = true;
    }
    public void Restart()
    {
        if (Input.GetKeyDown(KeyCode.Q)||restart)
        {
            canCount = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    public void ScoreMove()
    {
        print("");
        uiimage3.transform.position = emptyobject.transform.position;
        //Tween t= uiimage3.rectTransform.DOLocalMove(emptyobject.rectTransform.localPosition, 1.0f);
        print(emptyobject.transform.position);
        print(uiimage3.transform.position);
    }

}

