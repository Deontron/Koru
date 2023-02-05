using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public int[] moveRoutes;
    public int[] attackRoutes;

    private int blockId;

    public void CalculateTheRoutes(int characterNo)
    {
        blockId = transform.parent.GetComponent<BlockScript>().blockId;

        switch (characterNo)
        {
            case 1:
                moveRoutes[0] = blockId + 1;
                moveRoutes[1] = blockId - 1;
                moveRoutes[2] = blockId + 9;

                moveRoutes[0] = blockId + 10;
                moveRoutes[1] = blockId + 8;
                break;
        }

    }
}
