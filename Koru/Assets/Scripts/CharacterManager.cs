using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    private bool firstClick = true;
    private MatrisScript ms;
    private GameManager gm;
    private CharacterScript cs;
    public GameObject[] blocks;

    //The first block values
    private GameObject _block;
    private int _blockId;
    private int _characterNo;
    private char _team;

    private bool secondWhiteInfinity;
    private bool secondBlackInfinity;

    private void Start()
    {
        ms = GameObject.FindGameObjectWithTag("Matris").GetComponent<MatrisScript>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        cs = GetComponent<CharacterScript>();
        blocks = ms.blocks;
    }

    //This function is call when clicked any block
    public void PressedBlock(GameObject block, int blockId, int characterNo, char team)
    {
        if (firstClick)
        {
            //Take the values if this is the first click
            _block = block;
            _team = team;
            _blockId = blockId;
            _characterNo = characterNo;

            //Do nothing if this block is empty
            if (_block.GetComponent<BlockScript>().isFull && _block.GetComponent<BlockScript>().playPermission)
            {
                //Tell the first block to prepare
                MoveCharacter(_block);
                firstClick = false;
            }
        }
        else
        {
            if (block.GetComponent<BlockScript>().isInfinity && block.GetComponent<BlockScript>().team != _block.GetComponent<BlockScript>().team)
            {
                //Change the first block to infinity or hit the enemy infinity if the second block is infinity
                HitTheInfinity(_block, block);
            }
            else
            {
                //Send the second block to first block
                MoveCharacter(block);
            }

            //Reset the routes
            _block.GetComponent<BlockScript>().BackToNormal();
            firstClick = true;
            _block = null;
        }
    }

    //The top upgrade button calls this function
    public void BlackUpgradeButton()
    {
        if (_team == 'b' || (_blockId >= 63 && _team != 'w'))
        {
            //Call the upgrade function if the block is full
            if (gm.blackPlusAmount > 0 && _block != null && _characterNo >= 0 && _characterNo < 5 && !_block.GetComponent<BlockScript>().isInfinity)
            {
                gm.blackPlusAmount--;
                UpgradeButton();
            }
        }
    }

    //The bottom upgrade button calls this function
    public void WhiteUpgradeButton()
    {
        if (_team == 'w' || (_blockId <= 17 && _team != 'b'))
        {
            //Call the upgrade function if the block is full
            if (gm.whitePlusAmount > 0 && _block != null && _characterNo >= 0 && _characterNo < 5 && !_block.GetComponent<BlockScript>().isInfinity)
            {
                gm.whitePlusAmount--;
                UpgradeButton();
            }
        }
    }

    private void UpgradeButton()
    {
        UpgradeCharacter(_block, _blockId, _characterNo);

        //Reset the routes
        _block.GetComponent<BlockScript>().BackToNormal();

        firstClick = true;

        //Clear the value not to cause any error
        _block = null;

        if (gm.gameStarted)
        {
            //next player
            gm.FastNextTurn();
        }

        gm.UpdateThePlusTexts();
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

    public void DeployCharecter(GameObject block, int blockId)
    {
        //Deploy character if the clicked block is on top or bottom of the matris
        if (!(blockId > 17 && blockId < 63))
        {
            if (blockId >= 63)
            {
                //Make the character black if the clicked block is on the top of the matris
                block.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.black;
                block.GetComponent<BlockScript>().team = 'b';
            }
            else if (blockId <= 17)
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

    private void HitTheInfinity(GameObject firstBlock, GameObject secondBlock)
    {
        if (cs.attackRoutes.Contains(secondBlock.GetComponent<BlockScript>().blockId))
        {
            if (_team == 'w')
            {
                if (!secondWhiteInfinity && !firstBlock.GetComponent<BlockScript>().isInfinity && !secondBlock.GetComponent<InfinityScript>().isSecondInfinity)
                {
                    firstBlock.GetComponent<BlockScript>().ChangeToInfinity();
                    firstBlock.GetComponent<InfinityScript>().isSecondInfinity = true;
                    secondWhiteInfinity = true;
                }
                else
                {
                    for (int i = 0; i < blocks.Length; i++)
                    {
                        if (blocks[i].GetComponent<BlockScript>().isInfinity)
                        {
                            blocks[i].GetComponent<InfinityScript>().GetStun('b');
                        }
                    }
                }

            }
            else if (_team == 'b')
            {
                if (!secondBlackInfinity && !firstBlock.GetComponent<BlockScript>().isInfinity && !secondBlock.GetComponent<InfinityScript>().isSecondInfinity)
                {
                    firstBlock.GetComponent<BlockScript>().ChangeToInfinity();
                    firstBlock.GetComponent<InfinityScript>().isSecondInfinity = true;
                    secondBlackInfinity = true;
                }
                else
                {
                    for (int i = 0; i < blocks.Length; i++)
                    {
                        if (blocks[i].GetComponent<BlockScript>().isInfinity)
                        {
                            blocks[i].GetComponent<InfinityScript>().GetStun('w');
                        }
                    }
                }
            }

            //next player
            gm.FastNextTurn();
        }
    }
}
