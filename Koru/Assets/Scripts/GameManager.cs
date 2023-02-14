using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI textTop;
    public TextMeshProUGUI textBottom;

    private MatrisScript ms;
    private GameObject[] blocks;

    private float startTimer;
    private float startTime = 1;
    private bool gameStarted;
    private float queueTimer;
    private float queueTime;
    private bool playerOnesTurn;
    private bool countDown;

    void Start()
    {
        ms = GameObject.FindGameObjectWithTag("Matris").GetComponent<MatrisScript>();

        queueTime = 10;
        playerOnesTurn = true;
    }

    void Update()
    {
        if (!gameStarted)
        {
            FirstTimer();
        }
    }

    private void QueueTimer()
    {
        queueTimer += Time.deltaTime;
        if (queueTimer >= queueTime)
        {
            countDown = false;
            queueTimer = 0;
            QueueManager();
        }
    }

    private void QueueManager()
    {
        if (playerOnesTurn)
        {
            NextTurn('w');
            print("first");
        }
        else
        {
            NextTurn('b');
            print("second");
        }

        queueCounter++;
        playerOnesTurn = !playerOnesTurn;
        countDown = true;
    }

    private void NextTurn(char team)
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            if (blocks[i].GetComponent<BlockScript>().isFull && blocks[i].GetComponent<BlockScript>().team == team)
            {
                blocks[i].GetComponent<BlockScript>().playPermission = true;
            }

            if (blocks[i].GetComponent<BlockScript>().isFull && blocks[i].GetComponent<BlockScript>().team != team)
            {
                blocks[i].GetComponent<BlockScript>().playPermission = false;
            }
        }
    }

    public void FastNextTurn()
    {

    }

    private void FirstTimer()
    {
        startTimer += Time.deltaTime;
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

        StartCoroutine(Timer());
        NextTurn('w');

        gameStarted = true;
    }
}
