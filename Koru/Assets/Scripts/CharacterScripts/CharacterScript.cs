using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public List<int> moveRoutes = new List<int>();
    public List<int> attackRoutes = new List<int>();

    private GameObject[] blocks;
    private int blockX;
    private int blockY;

    //Used in calculating routes
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
                MoveAndAttackLine(1, 1, 2, team, false);
                MoveAndAttackLine(-1, -1, 2, team, false);
                MoveAndAttackLine(1, -1, 2, team, false);
                MoveAndAttackLine(-1, 1, 2, team, false);

                ControlAndAdd(attackRoutes, blockX + 1, blockY, team);
                ControlAndAdd(attackRoutes, blockX - 1, blockY, team);
                ControlAndAdd(attackRoutes, blockX, blockY + 1, team);
                ControlAndAdd(attackRoutes, blockX, blockY - 1, team);
                break;

            case 3:
                if (team == 'w')
                {
                    MoveAndAttackLine(0, 1, 3, team, true);
                    MoveAndAttackLine(1, 0, 2, team, true);
                    MoveAndAttackLine(-1, 0, 2, team, true);
                    MoveAndAttackLine(0, -1, 1, team, true);
                }
                else if (team == 'b')
                {
                    MoveAndAttackLine(0, -1, 3, team, true);
                    MoveAndAttackLine(1, 0, 2, team, true);
                    MoveAndAttackLine(-1, 0, 2, team, true);
                    MoveAndAttackLine(0, 1, 1, team, true);
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
                MoveAndAttackLine(1, 1, 4, team, true);
                MoveAndAttackLine(-1, -1, 4, team, true);
                MoveAndAttackLine(1, -1, 4, team, true);
                MoveAndAttackLine(-1, 1, 4, team, true);
                MoveAndAttackLine(0, 1, 4, team, true);
                MoveAndAttackLine(0, -1, 4, team, true);
                MoveAndAttackLine(1, 0, 4, team, true);
                MoveAndAttackLine(-1, 0, 4, team, true);
                break;

            case 6:
                MoveAndAttackLine(1, 1, 1, team, true);
                MoveAndAttackLine(-1, -1, 1, team, true);
                MoveAndAttackLine(1, -1, 1, team, true);
                MoveAndAttackLine(-1, 1, 1, team, true);
                MoveAndAttackLine(0, 1, 1, team, true);
                MoveAndAttackLine(0, -1, 1, team, true);
                MoveAndAttackLine(1, 0, 1, team, true);
                MoveAndAttackLine(-1, 0, 1, team, true);
                break;
        }
    }

    private void MoveAndAttackLine(int xIncrement, int yIncrement, int max, char team, bool attack)
    {
        x = blockX + xIncrement;
        y = blockY + yIncrement;
        idValue = (9 * y) + x;

        int i = 0;
        //Is it for attack and move or just move
        if (attack)
        {
            //Get all blocks in the order that the first block can go
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

                //Get the next target block based on increment value
                x += xIncrement;
                y += yIncrement;

                idValue = (9 * y) + x;
                i++;
            }
        }
        else
        {
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

    }

    private void ControlAndAdd(List<int> list, int valueX, int valueY, char team)
    {
        //Add the value with control
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
        //Add the value without control because already controlled
        int value = valueY * 9 + valueX;
        list.Add(value);
    }

    private void Equalize(List<int> firstList, List<int> secondList)
    {
        //Sync both routes
        for (int i = 0; i < firstList.Count; i++)
        {
            secondList.Add(firstList[i]);
        }
    }
}
