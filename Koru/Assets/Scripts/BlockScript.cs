using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class BlockScript : MonoBehaviour
{
    public char team;
    public int blockId;
    public int characterNo;
    public bool isInfinity;
    public bool isFull;
    public bool playPermission;
    public Color blockColor;
    public Color teamColor;

    public GameObject[] blocksToGo;

    private CharacterManager cm;
    private GameManager gm;
    private bool movePermission;
    private BlockScript secondBlock;
    private CharacterScript characterScript;

    private void Start()
    {
        playPermission = false;
        //Blocks deafult color
        blockColor = GetComponent<Image>().color;

        characterScript = GameObject.FindGameObjectWithTag("CharacterManager").GetComponent<CharacterScript>();

        cm = GameObject.FindGameObjectWithTag("CharacterManager").GetComponent<CharacterManager>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    //First, this function runs if you click any block
    public void OnMousePressed()
    {
        cm.PressedBlock(gameObject, blockId, characterNo, team);
        gm.GetActiveBlock(this);
    }

    //Character manager calls this function
    public void PrepareToMove()
    {
        //Get the routes the first block can go
        characterScript.CalculateTheRoutes(characterNo, team, blockId);

        teamColor = transform.GetChild(characterNo - 1).gameObject.GetComponent<Image>().color;

        //Change the color of the blocks we can go
        for (int j = 0; j < characterScript.attackRoutes.Count; j++)
        {
            if (blocksToGo[characterScript.attackRoutes[j]].GetComponent<BlockScript>().isFull)
            {
                blocksToGo[characterScript.attackRoutes[j]].GetComponent<Image>().color = Color.yellow;
            }
        }

        for (int j = 0; j < characterScript.moveRoutes.Count; j++)
        {
            if (!blocksToGo[characterScript.moveRoutes[j]].GetComponent<BlockScript>().isFull)
            {
                blocksToGo[characterScript.moveRoutes[j]].GetComponent<Image>().color = Color.green;
            }
        }
    }

    //Character manager calls this function
    public void Movement(GameObject block) //It gets the block(second block) to go
    {
        secondBlock = block.GetComponent<BlockScript>();

        //Controls if second block is me or is in my team
        if (blockId != secondBlock.blockId && team != secondBlock.team)
        {
            //Control if the first block can move or can attack
            if (secondBlock.isFull)
            {
                for (int j = 0; j < characterScript.attackRoutes.Count; j++)
                {
                    if (secondBlock.blockId == characterScript.attackRoutes[j])
                    {
                        movePermission = true;
                    }
                }
            }
            else
            {
                for (int j = 0; j < characterScript.moveRoutes.Count; j++)
                {
                    if (secondBlock.blockId == characterScript.moveRoutes[j])
                    {
                        movePermission = true;
                    }
                }
            }

            if (movePermission)
            {
                //Changes the values of first block
                transform.GetChild(characterNo - 1).gameObject.SetActive(false);
                isFull = false;

                //Tells the second block to change to the first block
                secondBlock.Change(characterNo, team, teamColor);

                characterNo = 0;
                team = 'n';

                //next player
                gm.FastNextTurn();

                if (isInfinity)
                {
                    secondBlock.isInfinity = true;
                    block.GetComponent<InfinityScript>().isSecondInfinity = true;
                    GetComponent<InfinityScript>().isSecondInfinity = false;
                    isInfinity = false;
                    gm.infinityBlocks.Remove(gameObject);
                    gm.infinityBlocks.Add(secondBlock.gameObject);
                    characterScript.Match(secondBlock.team, secondBlock.blockId);
                }

                movePermission = false;
            }

            BackToNormal();
        }
    }

    //The first block calls this function if this block is the second block
    public void Change(int _characterNo, char _team, Color _teamColor)
    {
        if (isFull)
        {
            //Change if the second block is full
            transform.GetChild(characterNo - 1).gameObject.SetActive(false);
            transform.GetChild(_characterNo - 1).gameObject.SetActive(true);
            transform.GetChild(_characterNo - 1).gameObject.GetComponent<Image>().color = _teamColor;
            team = _team;
            characterNo = _characterNo;
        }
        else
        {
            //Just move if the second block is empty
            team = _team;
            isFull = true;
            characterNo = _characterNo;
            transform.GetChild(_characterNo - 1).gameObject.GetComponent<Image>().color = _teamColor;
            transform.GetChild(_characterNo - 1).gameObject.SetActive(true);
        }
    }

    public void ChangeToInfinity()
    {
        characterNo = 6;

        for (int i = 0; i < (characterNo - 1); i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        transform.GetChild(characterNo - 1).gameObject.SetActive(true);
        transform.GetChild(characterNo - 1).gameObject.GetComponent<Image>().color = teamColor;
        isFull = true;
        isInfinity = true;

        //Look around if there is a friendly infinity
        characterScript.Match(team, blockId);

        gm.infinityBlocks.Add(gameObject);

        ////Stun to avoid being hit
        //if (gm.gameStarted)
        //{
        //    gameObject.GetComponent<InfinityScript>().GetStun(team);
        //}
    }

    public void BackToNormal()
    {
        //Change back the first block to normal
        for (int j = 0; j < characterScript.moveRoutes.Count; j++)
        {
            blocksToGo[characterScript.moveRoutes[j]].GetComponent<BlockScript>().BackToDefaultColor();
        }

        for (int j = 0; j < characterScript.attackRoutes.Count; j++)
        {
            blocksToGo[characterScript.attackRoutes[j]].GetComponent<BlockScript>().BackToDefaultColor();
        }

        //Clear the first blocks routes
        characterScript.moveRoutes.Clear();
        characterScript.attackRoutes.Clear();
    }

    public void BackToDefaultColor()
    {
        //Change the color to default color
        GetComponent<Image>().color = blockColor;
    }
}
