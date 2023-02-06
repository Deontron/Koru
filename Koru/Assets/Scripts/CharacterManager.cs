using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    private bool firstClick = true;

    private GameObject _block;
    private int _blockId;
    private int _characterNo;
    void Start()
    {

    }

    void Update()
    {

    }

    //This function is call when clicked any block
    public void PressedBlock(GameObject block, int blockId, int characterNo)
    {
        if (firstClick)
        {
            //Take the values if this is the first click
            _block = block;
            _blockId = blockId;
            _characterNo = characterNo;

            //Do nothing if this block is empty
            if (block.GetComponent<BlockScript>().isFull)
            {
                //Tell the first block to prepare
                MoveCharacter(_block);
                firstClick = false;
            }
        }
        else
        {
            //Send the second block to first block
            MoveCharacter(block);
            firstClick = true;
        }
    }

    //The upgrade button calls this function
    public void UpgradeButton()
    {
        //Call the upgrade function if the block is full
        if (_block != null && _characterNo >= 0)
        {
            UpgradeCharacter(_block, _blockId, _characterNo);
        }
        firstClick = true;
        //Clear the value not to cause any error
        _block = null;
    }

    private void MoveCharacter(GameObject block)
    {
        if (firstClick)
        {
            _block.GetComponent<BlockScript>().PrepareToMove();
        }
        else
        {
            _block.GetComponent<BlockScript>().Movement(block);
        }
    }

    public void UpgradeCharacter(GameObject block, int blockId, int characterNo)
    {
        //Control if the block is full
        if (block.GetComponent<BlockScript>().isFull)
        {
            //The biggest character is 5 but its id is (5-1)
            if (characterNo < 5)
            {
                //Level up the block
                block.transform.GetChild(characterNo - 1).gameObject.SetActive(false);
                block.transform.GetChild(characterNo).gameObject.SetActive(true);
                block.transform.GetChild(characterNo).gameObject.GetComponent<Image>().color = block.transform.GetChild(characterNo - 1).gameObject.GetComponent<Image>().color;
                block.GetComponent<BlockScript>().characterNo = characterNo + 1;
            }
        }
        else
        {
            DeployCharecter(block, blockId);
        }
    }

    private void DeployCharecter(GameObject block, int blockId)
    {
        //Deploy character if the clicked block is on top or bottom of the matris
        if (!(blockId > 17 && blockId < 63))
        {
            if (blockId <= 17)
            {
                //Make the character black if the clicked block is on the top of the matris
                block.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.black;
                block.GetComponent<BlockScript>().team = 'b';
            }
            else if (blockId >= 63)
            {
                //Make the character white if the clicked block is on the bottom of the matris
                block.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
                block.GetComponent<BlockScript>().team = 'w';
            }

            //Level up the block from 0 to 1
            block.transform.GetChild(0).gameObject.SetActive(true);
            block.GetComponent<BlockScript>().isFull = true;
            block.GetComponent<BlockScript>().characterNo = 1;
        }
    }
}
