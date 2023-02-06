using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //private CharacterManager cm;
    //private bool firstClick = true;

    //private GameObject _block;
    //private int _blockId;
    //private int _characterNo;
    //void Start()
    //{
    //    cm = GameObject.FindGameObjectWithTag("CharacterManager").GetComponent<CharacterManager>();
    //}

    //void Update()
    //{

    //}

    //public void Pressed(GameObject block, int blockId, int characterNo)
    //{
    //    if (firstClick)
    //    {
    //        _block = block;
    //        _blockId = blockId;
    //        _characterNo = characterNo;

    //        if (block.GetComponent<BlockScript>().isFull)
    //        {
    //            MoveCharacter(_block);
    //            firstClick = false;
    //        }
    //    }
    //    else
    //    {
    //        MoveCharacter(block);
    //        firstClick = true;
    //    }
    //}

    //public void UpgradeButton()
    //{
    //    if (_block != null)
    //    {

    //        cm.UpgradeCharacter(_block, _blockId, _characterNo);
    //    }
    //    firstClick = true;
    //}

    //private void MoveCharacter(GameObject block)
    //{
    //    if (firstClick)
    //    {
    //        _block.GetComponent<BlockScript>().PrepareToMove();
    //    }
    //    else
    //    {
    //        _block.GetComponent<BlockScript>().Movement(block);
    //    }
    //}
}
