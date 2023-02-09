using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfinityScript : MonoBehaviour
{
    private char _team;
    private CharacterScript cm;

    public void GetStun(char team)
    {
        _team = GetComponent<BlockScript>().team;

        if (_team == team)
        {
            GetComponent<Button>().interactable = false;
            StartCoroutine(Timer());
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(5);
        GetComponent<Button>().interactable = true;
    }
}
