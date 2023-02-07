using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class BlockScript : MonoBehaviour
{
    public char team;
    public int blockId;
    public int characterNo;
    public bool isFull;
    public Color blockColor;

    private Color teamColor;
    private CharacterManager cm;
    private GameManager gm;
    private bool movePermission;
    private BlockScript secondBlock;
    private CharacterScript characterScript;

    public GameObject[] blocksToGo;

    private void Start()
    {
        blockColor = GetComponent<Image>().color;
        characterScript = GetComponent<CharacterScript>();

        cm = GameObject.FindGameObjectWithTag("CharacterManager").GetComponent<CharacterManager>();
        //gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    //First, this function runs if you click any block
    public void OnMousePressed()
    {
        cm.PressedBlock(gameObject, blockId, characterNo);
        //gm.Pressed(gameObject, blockId, characterNo);
    }

    //Character manager calls this function
    public void PrepareToMove()
    {
        characterScript.CalculateTheRoutes(characterNo, team);

        for (int j = 0; j < characterScript.moveRoutes.Count; j++)
        {
            blocksToGo[characterScript.moveRoutes[j]].GetComponent<Image>().color = Color.green;
        }

        for (int j = 0; j < characterScript.attackRoutes.Count; j++)
        {
            blocksToGo[characterScript.attackRoutes[j]].GetComponent<Image>().color = Color.yellow;
        }

        print("prepared");
    }

    //Character manager calls this function
    public void Movement(GameObject block) //It gets the block(second block) to go
    {
        secondBlock = block.GetComponent<BlockScript>();

        //Controls if second block is me or is in my team
        if (blockId != secondBlock.blockId && team != secondBlock.team)
        {
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
                teamColor = transform.GetChild(characterNo - 1).gameObject.GetComponent<Image>().color;
                transform.GetChild(characterNo - 1).gameObject.SetActive(false);
                isFull = false;

                //Tells the second block to change to the first block
                secondBlock.Change(characterNo, team, teamColor);

                characterNo = 0;
                team = 'n';

                print("moved");
                movePermission = false;
            }

            for (int j = 0; j < characterScript.moveRoutes.Count; j++)
            {
                blocksToGo[characterScript.moveRoutes[j]].GetComponent<BlockScript>().BackToNormal();
            }

            for (int j = 0; j < characterScript.attackRoutes.Count; j++)
            {
                blocksToGo[characterScript.attackRoutes[j]].GetComponent<BlockScript>().BackToNormal();
            }

            characterScript.moveRoutes.Clear();
            characterScript.attackRoutes.Clear();
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

    public void BackToNormal()
    {
        GetComponent<Image>().color = blockColor;
    }
}
