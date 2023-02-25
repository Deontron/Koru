using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class InfinityScript : MonoBehaviour
{
    public bool stunned;
    private int stunDuration = 1;
    private int stunTurn;
    private char _team;
    private TurnMechanic tm;

    private void Start()
    {
        tm = GameObject.FindGameObjectWithTag("TurnMechanic").GetComponent<TurnMechanic>();
    }

    public void GetStun(char team)
    {
        _team = GetComponent<BlockScript>().team;

        if (_team == team)
        {
            GetComponent<Button>().interactable = false;
            stunned = true;
            stunTurn = tm.queueCounter;
        }
    }

    public void CheckTheQueue()
    {
        if ((stunTurn + stunDuration) == tm.queueCounter)
        {
            GetComponent<Button>().interactable = true;
            stunned = false;
        }
    }
}
