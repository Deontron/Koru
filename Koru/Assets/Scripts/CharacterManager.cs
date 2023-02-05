using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    public void PressedBlock(GameObject block, int blockId, int characterNo)
    {

    }

    public void UpgradeCharacter(GameObject block, int blockId, int characterNo)
    {
        if (block.GetComponent<BlockScript>().isFull)
        {
            if (characterNo < 5)
            {
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
        if (!(blockId > 17 && blockId < 63))
        {
            if (blockId <= 17)
            {
                block.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.black;
                block.GetComponent<BlockScript>().team = 'b';
            }
            else if (blockId >= 63)
            {
                block.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
                block.GetComponent<BlockScript>().team = 'w';
            }

            block.transform.GetChild(0).gameObject.SetActive(true);
            block.GetComponent<BlockScript>().isFull = true;
            block.GetComponent<BlockScript>().characterNo = 1;
        }
    }
}
