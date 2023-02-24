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

    private CharacterManager cm;
    private UIManager uim;
    private MatrisScript ms;
    private GameObject[] blocks;

    private BlockScript activeBlock;

    private float startTimer;
    private float startTime = 1;
    private bool firstStage;
    public bool gameStarted;
    private int[] firstBlocksID = { 11, 13, 15, 65, 67, 69 };

    private bool isGameGoing;
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
        cm = GameObject.FindGameObjectWithTag("CharacterManager").GetComponent<CharacterManager>();
    }

    void Update()
    {
        if (!firstStage)
        {
            //timer for first stage
            FirstTimer();
        }
        else
        {
            if (isGameGoing)
            {
                MainTimer();
            }

            //Finish the first stage
            if (!gameStarted && time.Minutes >= 1)
            {
                QueueManager();
                UnlockFirstCharacters();
                gameStarted = true;
            }
        }

        //Timer for players turn
        if (countDown)
        {
            QueueTimer();
        }
    }

    public void GameOver(char team)
    {
        uim.OpenGameOverPanel(team);
        isGameGoing = false;
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
        DeployFirstCharacters();

        //Set the game values
        queueTime = 200;
        playerOnesTurn = true;

        whitePlusAmount = 2;
        blackPlusAmount = 2;

        UpdateThePlusTexts();

        isGameGoing = true;
        firstStage = true;
    }

    private void DeployFirstCharacters()
    {
        //Deploy the first infinities
        blocks[4].GetComponent<BlockScript>().ChangeToInfinity();
        blocks[4].GetComponent<BlockScript>().team = 'w';
        blocks[76].GetComponent<BlockScript>().ChangeToInfinity();
        blocks[76].GetComponent<BlockScript>().team = 'b';

        //Deploy the first 1 characters
        foreach (int index in firstBlocksID)
        {
            cm.DeployCharecter(blocks[index], index);
        }

        LockFirstCharacters();
    }

    private void LockFirstCharacters()
    {
        //Lock the first characters that are not allowed to be upgraded
        foreach (int index in firstBlocksID)
        {
            blocks[index].GetComponent<Button>().interactable = false;
        }
    }

    private void UnlockFirstCharacters()
    {
        //Let the first characters to upgrade
        foreach (int index in firstBlocksID)
        {
            blocks[index].GetComponent<Button>().interactable = true;
        }
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

            //Reset the routes if time is over
            if (activeBlock != null)
            {
                activeBlock.BackToNormal();
                activeBlock = null;
            }

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
        //Let the player to play
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

        //Add a point in 8 turns
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
            UnlockFirstCharacters();
            gameStarted = true;
        }
    }

    public void GetActiveBlock(BlockScript block)
    {
        activeBlock = block;
    }
}
