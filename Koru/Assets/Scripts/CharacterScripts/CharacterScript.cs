using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public List<int> moveRoutes = new List<int>();
    public List<int> attackRoutes = new List<int>();

    private int blockId;

    public void CalculateTheRoutes(int characterNo, char team)
    {
        blockId = GetComponent<BlockScript>().blockId;

        switch (characterNo)
        {
            case 1:
                if (team == 'w')
                {
                    moveRoutes.Add(blockId + 1);
                    moveRoutes.Add(blockId - 1);
                    moveRoutes.Add(blockId - 9);

                    attackRoutes.Add(blockId - 10);
                    attackRoutes.Add(blockId - 8);
                }
                else if (team == 'b')
                {
                    moveRoutes.Add(blockId + 1);
                    moveRoutes.Add(blockId - 1);
                    moveRoutes.Add(blockId + 9);

                    attackRoutes.Add(blockId + 10);
                    attackRoutes.Add(blockId + 8);
                }
                break;
        }

        for (int i = 0; i < attackRoutes.Count; i++)
        {
            if (attackRoutes[i] < 0 || attackRoutes[i] > 80)
            {
                attackRoutes.RemoveAt(i);
            }
        }

        for (int i = 0; i < moveRoutes.Count; i++)
        {
            if (moveRoutes[i] < 0 || moveRoutes[i] > 80)
            {
                moveRoutes.RemoveAt(i);
            }
        }
    }
}
