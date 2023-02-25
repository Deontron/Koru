using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnMechanic : MonoBehaviour
{
    private GameObject[] blocks;
    private MatrisScript ms;
    private GameManager gm;

    public Button blackButton;
    public Button whiteButton;

    public int queueCounter;
    public bool countDown;
    public bool playerOnesTurn;

    void Start()
    {
        ms = GameObject.FindGameObjectWithTag("Matris").GetComponent<MatrisScript>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        blocks = ms.blocks;
    }

    public void QueueManager()
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

        //Add a point in ... turns
        if (queueCounter % 8 == 0 && queueCounter > 0)
        {
            gm.whitePlusAmount++;
            gm.blackPlusAmount++;
            gm.UpdateThePlusTexts();
        }

        foreach (GameObject block in blocks)
        {
            if (block.GetComponent<BlockScript>().isInfinity && block.GetComponent<InfinityScript>().stunned)
            {
                block.GetComponent<InfinityScript>().CheckTheQueue();
            }
        }
    }
}
