using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pauseUI;
    private static bool Ispause = false;
    void Start()
    {
        
    }
   
    // Update is called once per frame
    void Update()
    {
        Debug.Log(Ispause);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Ispause)
            {
                Resume();
            }
            else
            {
                PauseGame();
            }
        }
    }
    void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        Ispause = false;

    }
    void PauseGame()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        Ispause = true;
    }
}
