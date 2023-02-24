using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnMechanic : MonoBehaviour
{
    private GameObject[] blocks;
    private MatrisScript ms;
    private GameManager gm;

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
            gm.whiteButton.interactable = true;
            gm.blackButton.interactable = false;
        }
        else
        {
            gm.whiteButton.interactable = false;
            gm.blackButton.interactable = true;
        }

        //Add a point in 8 turns
        if (queueCounter % 8 == 0 && queueCounter > 0)
        {
            gm.whitePlusAmount++;
            gm.blackPlusAmount++;
            gm.UpdateThePlusTexts();
        }
    }
}
