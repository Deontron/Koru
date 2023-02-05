using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        block.transform.GetChild(0).gameObject.SetActive(true);
        block.GetComponent<BlockScript>().isFull = true;
        block.GetComponent<BlockScript>().characterNo = 1;
    }
}
