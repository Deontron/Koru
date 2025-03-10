using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatrisScript : MonoBehaviour
{
    public GameObject[] blocks = new GameObject[81];

    [SerializeField] private GameObject block;
    [SerializeField] private Color darkDolor;

    private GameObject spawnedBlock;
    private int width = 9;
    void Awake()
    {
        SetTheMatris();
    }

    private void SetTheMatris()
    {
        for (int i = 0; i < width * width; i++)
        {
            spawnedBlock = Instantiate(block, transform);
            spawnedBlock.GetComponent<BlockScript>().blockId = i;
            spawnedBlock.name = "Block " + i;

            blocks[i] = spawnedBlock;
            spawnedBlock.GetComponent<BlockScript>().blocksToGo = blocks;
            spawnedBlock.GetComponent<BlockScript>().teamColor = Color.white;

            if (i > 62)
            {
                spawnedBlock.GetComponent<BlockScript>().teamColor = Color.black;
            }

            if (i % 2 == 0)
            {
                spawnedBlock.GetComponent<Image>().color = darkDolor;
            }
        }
    }
}
