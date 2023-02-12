using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public List<int> moveRoutes = new List<int>();
    public List<int> attackRoutes = new List<int>();

    public GameObject[] blocks;
    private GameObject targetBlock;
    private int blockX;
    private int blockY;

    private int idValue;
    private int x = 0;
    private int y = 0;

    private void Start()
    {
        blocks = GetComponent<CharacterManager>().blocks;
    }

    public void CalculateTheRoutes(int characterNo, char team, int blockId)
    {
        //Separate the block id to x and y index
        blockX = blockId % 9;
        blockY = blockId / 9;

        switch (characterNo)
        {
            case 1:
                if (team == 'w')
                {
                    ControlAndAdd(moveRoutes, blockX - 1, blockY, team);
                    ControlAndAdd(moveRoutes, blockX + 1, blockY, team);
                    ControlAndAdd(moveRoutes, blockX, blockY + 1, team);

                    ControlAndAdd(attackRoutes, blockX - 1, blockY + 1, team);
                    ControlAndAdd(attackRoutes, blockX + 1, blockY + 1, team);
                }
                else if (team == 'b')
                {
                    ControlAndAdd(moveRoutes, blockX + 1, blockY, team);
                    ControlAndAdd(moveRoutes, blockX - 1, blockY, team);
                    ControlAndAdd(moveRoutes, blockX, blockY - 1, team);

                    ControlAndAdd(attackRoutes, blockX + 1, blockY - 1, team);
                    ControlAndAdd(attackRoutes, blockX - 1, blockY - 1, team);
                }
                break;

            case 2:
                MoveLine(1, 1, 2);
                MoveLine(-1, -1, 2);
                MoveLine(1, -1, 2);
                MoveLine(-1, 1, 2);

                ControlAndAdd(attackRoutes, blockX + 1, blockY, team);
                ControlAndAdd(attackRoutes, blockX - 1, blockY, team);
                ControlAndAdd(attackRoutes, blockX, blockY + 1, team);
                ControlAndAdd(attackRoutes, blockX, blockY - 1, team);
                break;

            case 3:
                if (team == 'w')
                {
                    MoveAndAttackLine(0, 1, 3, team);
                    MoveAndAttackLine(1, 0, 2, team);
                    MoveAndAttackLine(-1, 0, 2, team);
                    MoveAndAttackLine(0, -1, 1, team);
                }
                else if (team == 'b')
                {
                    MoveAndAttackLine(0, -1, 3, team);
                    MoveAndAttackLine(1, 0, 2, team);
                    MoveAndAttackLine(-1, 0, 2, team);
                    MoveAndAttackLine(0, 1, 1, team);
                }

                break;

            case 4:
                ControlAndAdd(moveRoutes, blockX + 1, blockY + 3, team);
                ControlAndAdd(moveRoutes, blockX - 1, blockY + 3, team);
                ControlAndAdd(moveRoutes, blockX + 1, blockY - 3, team);
                ControlAndAdd(moveRoutes, blockX - 1, blockY - 3, team);
                ControlAndAdd(moveRoutes, blockX + 2, blockY + 1, team);
                ControlAndAdd(moveRoutes, blockX - 2, blockY + 1, team);
                ControlAndAdd(moveRoutes, blockX + 2, blockY - 1, team);
                ControlAndAdd(moveRoutes, blockX - 2, blockY - 1, team);

                Equalize(moveRoutes, attackRoutes);
                break;

            case 5:
                MoveAndAttackLine(1, 1, 4, team);
                MoveAndAttackLine(-1, -1, 4, team);
                MoveAndAttackLine(1, -1, 4, team);
                MoveAndAttackLine(-1, 1, 4, team);
                MoveAndAttackLine(0, 1, 4, team);
                MoveAndAttackLine(0, -1, 4, team);
                MoveAndAttackLine(1, 0, 4, team);
                MoveAndAttackLine(-1, 0, 4, team);

                break;

            case 6:
                MoveAndAttackLine(1, 1, 1, team);
                MoveAndAttackLine(-1, -1, 1, team);
                MoveAndAttackLine(1, -1, 1, team);
                MoveAndAttackLine(-1, 1, 1, team);
                MoveAndAttackLine(0, 1, 1, team);
                MoveAndAttackLine(0, -1, 1, team);
                MoveAndAttackLine(1, 0, 1, team);
                MoveAndAttackLine(-1, 0, 1, team);

                break;
        }

    }

    private void MoveAndAttackLine(int xIncrement, int yIncrement, int max, char team)
    {
        x = blockX + xIncrement;
        y = blockY + yIncrement;
        idValue = (9 * y) + x;

        int i = 0;
        while (x < 9 && x >= 0 && y < 9 && y >= 0 && i < max)
        {
            if (blocks[idValue].GetComponent<BlockScript>().isFull)
            {
                if (team != blocks[idValue].GetComponent<BlockScript>().team)
                {
                    Add(attackRoutes, x, y);
                }
                break;
            }
            else
            {
                Add(moveRoutes, x, y);
            }

            x += xIncrement;
            y += yIncrement;

            idValue = (9 * y) + x;
            i++;
        }
    }

    private void MoveLine(int xIncrement, int yIncrement, int max)
    {
        x = blockX + xIncrement;
        y = blockY + yIncrement;
        idValue = (9 * y) + x;

        int i = 0;
        while (x < 9 && x >= 0 && y < 9 && y >= 0 && i < max)
        {
            if (!blocks[idValue].GetComponent<BlockScript>().isFull)
            {
                Add(moveRoutes, x, y);
            }
            else
            {
                break;
            }

            x += xIncrement;
            y += yIncrement;

            idValue = (9 * y) + x;
            i++;
        }
    }


    private void ControlAndAdd(List<int> list, int valueX, int valueY, char team)
    {
        if (valueX < 9 && valueX >= 0 && valueY < 9 && valueY >= 0)
        {
            int value = valueY * 9 + valueX;
            if (team != blocks[value].GetComponent<BlockScript>().team)
            {
                list.Add(value);
            }
        }
    }

    private void Add(List<int> list, int valueX, int valueY)
    {
        int value = valueY * 9 + valueX;
        list.Add(value);
    }

    private void Equalize(List<int> firstList, List<int> secondList)
    {
        for (int i = 0; i < firstList.Count; i++)
        {
            secondList.Add(firstList[i]);
        }
    }
}
