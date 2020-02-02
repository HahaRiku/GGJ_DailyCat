using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class ScenceLoader : MonoBehaviour
{
    [SerializeField] Button btn;
    [SerializeField] Button btn2;
    private bool isBtnSelected = true;

    private void Start()
    {
        btn.Select();
    }
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    public void LoadStartScene()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void SelectChanged()
    {
        if (isBtnSelected)
        {
            btn2.Select();
            isBtnSelected = false;
        }
        else
        {
            btn.Select();
            isBtnSelected = true;
        }
    }
    public void btnTrig()
    {
        if (isBtnSelected)
        {
            LoadStartScene();
        }
        else
        {
            QuitGame();
        }
    }
}
