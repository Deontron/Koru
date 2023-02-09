using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public List<int> moveRoutes = new List<int>();
    public List<int> attackRoutes = new List<int>();

    private int blockId;
    private int blockX;
    private int blockY;

    public void CalculateTheRoutes(int characterNo, char team)
    {
        //Separate the block id to x and y index
        blockId = GetComponent<BlockScript>().blockId;
        blockX = blockId % 9;
        blockY = blockId / 9;

        switch (characterNo)
        {
            case 1:
                if (team == 'w')
                {
                    ControlAndAdd(moveRoutes, blockX + 1, blockY);
                    ControlAndAdd(moveRoutes, blockX - 1, blockY);
                    ControlAndAdd(moveRoutes, blockX, blockY + 1);

                    ControlAndAdd(attackRoutes, blockX + 1, blockY + 1);
                    ControlAndAdd(attackRoutes, blockX - 1, blockY + 1);
                }
                else if (team == 'b')
                {
                    ControlAndAdd(moveRoutes, blockX + 1, blockY);
                    ControlAndAdd(moveRoutes, blockX - 1, blockY);
                    ControlAndAdd(moveRoutes, blockX, blockY - 1);

                    ControlAndAdd(attackRoutes, blockX + 1, blockY - 1);
                    ControlAndAdd(attackRoutes, blockX - 1, blockY - 1);
                }
                break;

            case 2:
                if (team == 'w')
                {
                    ControlAndAdd(moveRoutes, blockX + 2, blockY + 2);
                    ControlAndAdd(moveRoutes, blockX - 2, blockY - 2);
                    ControlAndAdd(moveRoutes, blockX - 2, blockY + 2);
                    ControlAndAdd(moveRoutes, blockX + 2, blockY - 2);
                    ControlAndAdd(moveRoutes, blockX + 1, blockY + 1);
                    ControlAndAdd(moveRoutes, blockX - 1, blockY - 1);
                    ControlAndAdd(moveRoutes, blockX - 1, blockY + 1);
                    ControlAndAdd(moveRoutes, blockX + 1, blockY - 1);

                    ControlAndAdd(attackRoutes, blockX + 1, blockY);
                    ControlAndAdd(attackRoutes, blockX - 1, blockY);
                    ControlAndAdd(attackRoutes, blockX, blockY + 1);
                    ControlAndAdd(attackRoutes, blockX, blockY - 1);
                }
                else if (team == 'b')
                {
                    ControlAndAdd(moveRoutes, blockX + 2, blockY + 2);
                    ControlAndAdd(moveRoutes, blockX - 2, blockY - 2);
                    ControlAndAdd(moveRoutes, blockX - 2, blockY + 2);
                    ControlAndAdd(moveRoutes, blockX + 2, blockY - 2);
                    ControlAndAdd(moveRoutes, blockX + 1, blockY + 1);
                    ControlAndAdd(moveRoutes, blockX - 1, blockY - 1);
                    ControlAndAdd(moveRoutes, blockX - 1, blockY + 1);
                    ControlAndAdd(moveRoutes, blockX + 1, blockY - 1);

                    ControlAndAdd(attackRoutes, blockX + 1, blockY);
                    ControlAndAdd(attackRoutes, blockX - 1, blockY);
                    ControlAndAdd(attackRoutes, blockX, blockY + 1);
                    ControlAndAdd(attackRoutes, blockX, blockY - 1);
                }
                break;

            case 3:
                if (team == 'w')
                {
                    ControlAndAdd(moveRoutes, blockX + 1, blockY);
                    ControlAndAdd(moveRoutes, blockX - 1, blockY);
                    ControlAndAdd(moveRoutes, blockX + 2, blockY);
                    ControlAndAdd(moveRoutes, blockX - 2, blockY);
                    ControlAndAdd(moveRoutes, blockX, blockY + 1);
                    ControlAndAdd(moveRoutes, blockX, blockY + 2);
                    ControlAndAdd(moveRoutes, blockX, blockY + 3);
                    ControlAndAdd(moveRoutes, blockX, blockY - 1);

                    attackRoutes = moveRoutes;
                }
                else if (team == 'b')
                {
                    ControlAndAdd(moveRoutes, blockX + 1, blockY);
                    ControlAndAdd(moveRoutes, blockX - 1, blockY);
                    ControlAndAdd(moveRoutes, blockX + 2, blockY);
                    ControlAndAdd(moveRoutes, blockX - 2, blockY);
                    ControlAndAdd(moveRoutes, blockX, blockY - 1);
                    ControlAndAdd(moveRoutes, blockX, blockY - 2);
                    ControlAndAdd(moveRoutes, blockX, blockY - 3);
                    ControlAndAdd(moveRoutes, blockX, blockY + 1);

                    attackRoutes = moveRoutes;
                }
                break;

            case 4:
                if (team == 'w')
                {
                    ControlAndAdd(moveRoutes, blockX + 1, blockY + 3);
                    ControlAndAdd(moveRoutes, blockX - 1, blockY + 3);
                    ControlAndAdd(moveRoutes, blockX + 1, blockY - 3);
                    ControlAndAdd(moveRoutes, blockX - 1, blockY - 3);
                    ControlAndAdd(moveRoutes, blockX + 2, blockY + 1);
                    ControlAndAdd(moveRoutes, blockX - 2, blockY + 1);
                    ControlAndAdd(moveRoutes, blockX + 2, blockY - 1);
                    ControlAndAdd(moveRoutes, blockX - 2, blockY - 1);

                    attackRoutes = moveRoutes;
                }
                else if (team == 'b')
                {
                    ControlAndAdd(moveRoutes, blockX + 1, blockY + 3);
                    ControlAndAdd(moveRoutes, blockX - 1, blockY + 3);
                    ControlAndAdd(moveRoutes, blockX + 1, blockY - 3);
                    ControlAndAdd(moveRoutes, blockX - 1, blockY - 3);
                    ControlAndAdd(moveRoutes, blockX + 2, blockY + 1);
                    ControlAndAdd(moveRoutes, blockX - 2, blockY + 1);
                    ControlAndAdd(moveRoutes, blockX + 2, blockY - 1);
                    ControlAndAdd(moveRoutes, blockX - 2, blockY - 1);

                    attackRoutes = moveRoutes;
                }
                break;

            case 5:
                if (team == 'w')
                {
                    ControlAndAdd(moveRoutes, blockX + 1, blockY);
                    ControlAndAdd(moveRoutes, blockX + 2, blockY);
                    ControlAndAdd(moveRoutes, blockX + 3, blockY);
                    ControlAndAdd(moveRoutes, blockX + 4, blockY);
                    ControlAndAdd(moveRoutes, blockX - 1, blockY);
                    ControlAndAdd(moveRoutes, blockX - 2, blockY);
                    ControlAndAdd(moveRoutes, blockX - 3, blockY);
                    ControlAndAdd(moveRoutes, blockX - 4, blockY);
                    ControlAndAdd(moveRoutes, blockX, blockY + 1);
                    ControlAndAdd(moveRoutes, blockX, blockY + 2);
                    ControlAndAdd(moveRoutes, blockX, blockY + 3);
                    ControlAndAdd(moveRoutes, blockX, blockY + 4);
                    ControlAndAdd(moveRoutes, blockX, blockY - 1);
                    ControlAndAdd(moveRoutes, blockX, blockY - 2);
                    ControlAndAdd(moveRoutes, blockX, blockY - 3);
                    ControlAndAdd(moveRoutes, blockX, blockY - 4);
                    ControlAndAdd(moveRoutes, blockX + 1, blockY + 1);
                    ControlAndAdd(moveRoutes, blockX + 2, blockY + 2);
                    ControlAndAdd(moveRoutes, blockX + 3, blockY + 3);
                    ControlAndAdd(moveRoutes, blockX + 4, blockY + 4);
                    ControlAndAdd(moveRoutes, blockX - 1, blockY - 1);
                    ControlAndAdd(moveRoutes, blockX - 2, blockY - 2);
                    ControlAndAdd(moveRoutes, blockX - 3, blockY - 3);
                    ControlAndAdd(moveRoutes, blockX - 4, blockY - 4);
                    ControlAndAdd(moveRoutes, blockX - 1, blockY + 1);
                    ControlAndAdd(moveRoutes, blockX - 2, blockY + 2);
                    ControlAndAdd(moveRoutes, blockX - 3, blockY + 3);
                    ControlAndAdd(moveRoutes, blockX - 4, blockY + 4);
                    ControlAndAdd(moveRoutes, blockX + 1, blockY - 1);
                    ControlAndAdd(moveRoutes, blockX + 2, blockY - 2);
                    ControlAndAdd(moveRoutes, blockX + 3, blockY - 3);
                    ControlAndAdd(moveRoutes, blockX + 4, blockY - 4);

                    attackRoutes = moveRoutes;
                }
                else if (team == 'b')
                {
                    ControlAndAdd(moveRoutes, blockX + 1, blockY);
                    ControlAndAdd(moveRoutes, blockX + 2, blockY);
                    ControlAndAdd(moveRoutes, blockX + 3, blockY);
                    ControlAndAdd(moveRoutes, blockX + 4, blockY);
                    ControlAndAdd(moveRoutes, blockX - 1, blockY);
                    ControlAndAdd(moveRoutes, blockX - 2, blockY);
                    ControlAndAdd(moveRoutes, blockX - 3, blockY);
                    ControlAndAdd(moveRoutes, blockX - 4, blockY);
                    ControlAndAdd(moveRoutes, blockX, blockY + 1);
                    ControlAndAdd(moveRoutes, blockX, blockY + 2);
                    ControlAndAdd(moveRoutes, blockX, blockY + 3);
                    ControlAndAdd(moveRoutes, blockX, blockY + 4);
                    ControlAndAdd(moveRoutes, blockX, blockY - 1);
                    ControlAndAdd(moveRoutes, blockX, blockY - 2);
                    ControlAndAdd(moveRoutes, blockX, blockY - 3);
                    ControlAndAdd(moveRoutes, blockX, blockY - 4);
                    ControlAndAdd(moveRoutes, blockX + 1, blockY + 1);
                    ControlAndAdd(moveRoutes, blockX + 2, blockY + 2);
                    ControlAndAdd(moveRoutes, blockX + 3, blockY + 3);
                    ControlAndAdd(moveRoutes, blockX + 4, blockY + 4);
                    ControlAndAdd(moveRoutes, blockX - 1, blockY - 1);
                    ControlAndAdd(moveRoutes, blockX - 2, blockY - 2);
                    ControlAndAdd(moveRoutes, blockX - 3, blockY - 3);
                    ControlAndAdd(moveRoutes, blockX - 4, blockY - 4);
                    ControlAndAdd(moveRoutes, blockX - 1, blockY + 1);
                    ControlAndAdd(moveRoutes, blockX - 2, blockY + 2);
                    ControlAndAdd(moveRoutes, blockX - 3, blockY + 3);
                    ControlAndAdd(moveRoutes, blockX - 4, blockY + 4);
                    ControlAndAdd(moveRoutes, blockX + 1, blockY - 1);
                    ControlAndAdd(moveRoutes, blockX + 2, blockY - 2);
                    ControlAndAdd(moveRoutes, blockX + 3, blockY - 3);
                    ControlAndAdd(moveRoutes, blockX + 4, blockY - 4);

                    attackRoutes = moveRoutes;
                }
                break;
        }
    }

    private void ControlAndAdd(List<int> list, int valueX, int valueY)
    {
        if (valueX < 9 && valueX >= 0 && valueY < 9 && valueY >= 0)
        {
            int value = valueY * 9 + valueX;
            list.Add(value);
        }
    }
}
