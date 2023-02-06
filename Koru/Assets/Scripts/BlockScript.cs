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
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void OnMousePressed()
    {
        cm.PressedBlock(gameObject, blockId, characterNo);
        //gm.Pressed(gameObject, blockId, characterNo);
    }

    public void PrepareToMove()
    {
        print("prepared");
    }

    public void Movement(GameObject block)
    {
        if (blockId != block.GetComponent<BlockScript>().blockId && team != block.GetComponent<BlockScript>().team)
        {
            color = transform.GetChild(characterNo - 1).gameObject.GetComponent<Image>().color;
            transform.GetChild(characterNo - 1).gameObject.SetActive(false);
            isFull = false;

            block.GetComponent<BlockScript>().Change(characterNo, team, color);

            characterNo = 0;
            team = 'n';

            print("moved");
        }
    }

    public void Change(int _characterNo, char _team, Color _color)
    {
        if (isFull)
        {
            transform.GetChild(characterNo - 1).gameObject.SetActive(false);
            transform.GetChild(_characterNo - 1).gameObject.SetActive(true);
            transform.GetChild(_characterNo - 1).gameObject.GetComponent<Image>().color = _color;
            team = _team;
        }
        else
        {
            team = _team;
            isFull = true;
            characterNo = _characterNo;
            transform.GetChild(_characterNo - 1).gameObject.GetComponent<Image>().color = _color;
            transform.GetChild(_characterNo - 1).gameObject.SetActive(true);
        }

    }
}
