using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    public bool isWhite;
    public int blockId;
    public int characterNo;
    public bool isFull;

    private CharacterManager cm;
    private GameManager gm;

    private void Start()
    {
        cm = GameObject.FindGameObjectWithTag("CharacterManager").GetComponent<CharacterManager>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void OnMousePressed()
    {
        //cm.PressedBlock(gameObject, blockId, characterNo);
        gm.Pressed(gameObject, blockId, characterNo);
    }

    public void PrepareToMove()
    {
        print("prepared");
    }

    public void Movement(GameObject block)
    {
        block.GetComponent<BlockScript>().Change(characterNo);

        transform.GetChild(characterNo - 1).gameObject.SetActive(false);
        characterNo = 0;
        isFull = false;


        print("moved");
    }

    public void Change(int _characterNo)
    {
        if (isFull)
        {
            transform.GetChild(characterNo - 1).gameObject.SetActive(false);
            transform.GetChild(_characterNo - 1).gameObject.SetActive(true);
        }
        else
        {
            isFull = true;
            characterNo = _characterNo;
            transform.GetChild(_characterNo - 1).gameObject.SetActive(true);
        }

    }
}
