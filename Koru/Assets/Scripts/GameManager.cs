using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI textBottom;
    public TextMeshProUGUI textTop;

    private MatrisScript ms;
    private GameObject[] blocks;
    private float startTimer;
    private float startTime = 1;
    private bool gameStarted;
    private float queueTimer;
    private float queueTime;
    private bool playerOnesTurn;

    void Start()
    {
        ms = GameObject.FindGameObjectWithTag("Matris").GetComponent<MatrisScript>();

        queueTime = 5;
        playerOnesTurn = true;
    }

    void Update()
    {
        if (!gameStarted)
        {
            FirstTimer();
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(queueTime);

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

        playerOnesTurn = !playerOnesTurn;
        StartCoroutine(Timer());
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
