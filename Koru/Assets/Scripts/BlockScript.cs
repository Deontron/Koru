using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{
    public int blockId;
    public int characterNo;
    public bool isFull;

    private CharacterManager cm;

    private void Start()
    {
        cm = GameObject.FindGameObjectWithTag("CharacterManager").GetComponent<CharacterManager>();
    }

    public void OnMousePressed()
    {
        cm.UpgradeCharacter(gameObject, blockId, characterNo);
    }
}
