using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI textTop;
    public TextMeshProUGUI textBottom;

    public TextMeshProUGUI whitePlusText;
    public TextMeshProUGUI blackPlusText;
    public int whitePlusAmount;
    public int blackPlusAmount;

    private TurnMechanic tm;
    private CharacterManager cm;
    private UIManager uim;
    private MatrisScript ms;
    private GameObject[] blocks;
    public List<GameObject> infinityBlocks;

    private BlockScript activeBlock;

    private bool firstStage;
    public bool gameStarted;
    private int[] firstBlocksID = { 11, 13, 15, 65, 67, 69 };

    private bool isGameGoing;
    private float mainTimer;
    private TimeSpan time;

    private float queueTimer;
    private float queueTime;

    void Start()
    {
        ms = GameObject.FindGameObjectWithTag("Matris").GetComponent<MatrisScript>();
        uim = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        cm = GameObject.FindGameObjectWithTag("CharacterManager").GetComponent<CharacterManager>();
        tm = GameObject.FindGameObjectWithTag("TurnMechanic").GetComponent<TurnMechanic>();

        //For the first countdown
        mainTimer = 4;
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
                tm.QueueManager();
                UnlockFirstCharacters();
                gameStarted = true;
            }
        }

        //Timer for players turn
        if (tm.countDown)
        {
            QueueTimer();
        }
    }

    public void GameOver(char team)
    {
        uim.OpenGameOverPanel(team);
        isGameGoing = false;
    }

    private void StartTheGame()
    {
        DeployFirstCharacters();

        //Set the game values
        queueTime = 20;
        tm.playerOnesTurn = true;

        whitePlusAmount = 25;
        blackPlusAmount = 25;

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

        //infinityBlocks.Add(blocks[4]);
        //infinityBlocks.Add(blocks[76]);

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

    private void FirstTimer()
    {
        mainTimer -= Time.deltaTime;
        UpdateTimerText();

        if (mainTimer <= 0)
        {
            blocks = ms.blocks;
            mainTimer = 0;
            StartTheGame();
        }
    }

    private void MainTimer()
    {
        mainTimer += Time.deltaTime;

        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        time = TimeSpan.FromSeconds(mainTimer);

        textTop.text = time.Minutes.ToString() + " : " + time.Seconds.ToString();
        textBottom.text = time.Minutes.ToString() + " : " + time.Seconds.ToString();
    }

    private void QueueTimer()
    {
        queueTimer += Time.deltaTime;
        if (queueTimer >= queueTime)
        {
            tm.countDown = false;
            queueTimer = 0;

            //Reset the routes if time is over
            if (activeBlock != null)
            {
                activeBlock.BackToNormal();
                activeBlock = null;
            }

            tm.QueueManager();
        }
    }

    public void FastNextTurn()
    {
        queueTimer = 0;
        tm.QueueManager();
    }

    public void UpdateThePlusTexts()
    {
        whitePlusText.text = whitePlusAmount.ToString();
        blackPlusText.text = blackPlusAmount.ToString();

        if (whitePlusAmount <= 0 && blackPlusAmount <= 0 && !gameStarted)
        {
            tm.QueueManager();
            UnlockFirstCharacters();
            gameStarted = true;
        }
    }

    public void GetActiveBlock(BlockScript block)
    {
        activeBlock = block;
    }
}
