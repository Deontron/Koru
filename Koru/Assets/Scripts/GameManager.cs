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
            print("started");
            blocks = ms.blocks;
            StartTheGame();
            gameStarted = true;
        }
    }
    private void StartTheGame()
    {
        blocks[0].GetComponent<BlockScript>().ChangeToInfinity();
        blocks[0].GetComponent<BlockScript>().team = 'w';
        blocks[80].GetComponent<BlockScript>().ChangeToInfinity();
        blocks[80].GetComponent<BlockScript>().team = 'b';
    }
}
