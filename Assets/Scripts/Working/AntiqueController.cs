using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AntiqueController : MonoBehaviour
{
    [SerializeField] Text scoreText = null;
    [SerializeField] GameObject antique = null;

    //state variable
    int currentScore = 0;

    [SerializeField] float timeToReCreate = 5f;
    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<AntiqueController>().Length;
        if (gameStatusCount > 1)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        scoreText.text = currentScore.ToString();
    }

    public void AddToScore(int pointsPerObjectFixed)
    {
        currentScore = currentScore + pointsPerObjectFixed;
        scoreText.text = currentScore.ToString();
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
        yield return new WaitForSeconds(timeToReCreate);
        Instantiate(antique, position, transform.rotation);
    }
}
