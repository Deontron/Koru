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

    public Button blackButton;
    public Button whiteButton;

    public TextMeshProUGUI whitePlusText;
    public TextMeshProUGUI blackPlusText;
    public int whitePlusAmount;
    public int blackPlusAmount;

    private UIManager uim;
    private MatrisScript ms;
    private GameObject[] blocks;

    private float startTimer;
    private float startTime = 1;
    private bool firstStage;
    public bool gameStarted;

    private float mainTimer;
    private TimeSpan time;

    public int queueCounter;
    private float queueTimer;
    private float queueTime;
    private bool playerOnesTurn;
    private bool countDown;

    void Start()
    {
        ms = GameObject.FindGameObjectWithTag("Matris").GetComponent<MatrisScript>();
        uim = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();

        queueTime = 20;
        playerOnesTurn = true;
    }

    void Update()
    {
        if (!firstStage)
        {
            FirstTimer();
        }
        else
        {
            MainTimer();

            if (!gameStarted && time.Minutes >= 1)
            {
                QueueManager();
                gameStarted = true;
            }
        }

        if (countDown)
        {
            QueueTimer();
        }
    }

    public void GameOver(char team)
    {
        uim.OpenGameOverPanel(team);
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

        whitePlusAmount = 2;
        blackPlusAmount = 2;

        UpdateThePlusTexts();

        firstStage = true;
    }

    private void MainTimer()
    {
        mainTimer += Time.deltaTime;

        time = TimeSpan.FromSeconds(mainTimer);

        textTop.text = time.Minutes.ToString() + " : " + time.Seconds.ToString();
        textBottom.text = time.Minutes.ToString() + " : " + time.Seconds.ToString();
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
        }
        else
        {
            NextTurn('b');
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

        if (team == 'w')
        {
            whiteButton.interactable = true;
            blackButton.interactable = false;
        }
        else
        {
            whiteButton.interactable = false;
            blackButton.interactable = true;
        }

        if (queueCounter % 8 == 0 && queueCounter > 0)
        {
            whitePlusAmount++;
            blackPlusAmount++;
            UpdateThePlusTexts();
        }
    }

    public void FastNextTurn()
    {
        queueTimer = 0;
        QueueManager();
    }

    public void UpdateThePlusTexts()
    {
        whitePlusText.text = whitePlusAmount.ToString();
        blackPlusText.text = blackPlusAmount.ToString();

        if (whitePlusAmount <= 0 && blackPlusAmount <= 0 && !gameStarted)
        {
            QueueManager();
            gameStarted = true;
        }
    }
}
