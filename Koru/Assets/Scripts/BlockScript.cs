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

    private Color color;
    private CharacterManager cm;
    private GameManager gm;

    private void Start()
    {
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
        print("prepared");
    }

    //Character manager calls this function
    public void Movement(GameObject block) //It gets the block(second block) to go
    {
        //Controls if second block is me or is in my team
        if (blockId != block.GetComponent<BlockScript>().blockId && team != block.GetComponent<BlockScript>().team)
        {
            //Changes the values of first block
            color = transform.GetChild(characterNo - 1).gameObject.GetComponent<Image>().color;
            transform.GetChild(characterNo - 1).gameObject.SetActive(false);
            isFull = false;

            //Tells the second block to change to the first block
            block.GetComponent<BlockScript>().Change(characterNo, team, color);

            characterNo = 0;
            team = 'n';

            print("moved");
        }
    }

    //The first block calls this function if this block is the second block
    public void Change(int _characterNo, char _team, Color _color)
    {
        if (isFull)
        {
            //Change if the second block is full
            transform.GetChild(characterNo - 1).gameObject.SetActive(false);
            transform.GetChild(_characterNo - 1).gameObject.SetActive(true);
            transform.GetChild(_characterNo - 1).gameObject.GetComponent<Image>().color = _color;
            team = _team;
            characterNo = _characterNo;
        }
        else
        {
            //Just move if the second block is empty
            team = _team;
            isFull = true;
            characterNo = _characterNo;
            transform.GetChild(_characterNo - 1).gameObject.GetComponent<Image>().color = _color;
            transform.GetChild(_characterNo - 1).gameObject.SetActive(true);
        }

    }
}
