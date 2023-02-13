using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private MatrisScript ms;
    private GameObject[] blocks;
    private float startTimer;
    private float startTime = 1;
    private bool gameStarted;

    void Start()
    {
        ms = GameObject.FindGameObjectWithTag("Matris").GetComponent<MatrisScript>();
    }

    void Update()
    {
        if (!gameStarted)
        {
            Timer();
            startTimer += Time.deltaTime;
        }
    }

    private void Timer()
    {
        if (startTimer >= startTime)
        {
            blocks = ms.blocks;
            StartTheGame();
        }
    }
    private void StartTheGame()
    {
        blocks[4].GetComponent<BlockScript>().ChangeToInfinity();
        blocks[4].GetComponent<BlockScript>().team = 'w';
        blocks[76].GetComponent<BlockScript>().ChangeToInfinity();
        blocks[76].GetComponent<BlockScript>().team = 'b';

        gameStarted = true;
    }
}
