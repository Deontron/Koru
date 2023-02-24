using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class InfinityScript : MonoBehaviour
{
    private TurnMechanic tm;
    private int stunDuration = 2;
    private int currentTurn;
    private char _team;

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
            currentTurn = tm.queueCounter;
            StartCoroutine(Timer());
        }
    }


    private void CheckTheQueue()
    {
        if ((currentTurn + stunDuration) == tm.queueCounter)
        {
            GetComponent<Button>().interactable = true;
        }
    }

    IEnumerator Timer()
    {
        while ((currentTurn + stunDuration) != tm.queueCounter)
        {
            yield return new WaitForSeconds(0.5f);
            CheckTheQueue();
            Debug.Log("sea");
        }
    }


}
