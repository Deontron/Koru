using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BlockScript : MonoBehaviour
{
    public int blockId;

    [SerializeField] private Color lightColor;

    private Color defaultColor;

    private void Start()
    {
        defaultColor = GetComponent<Image>().color;
    }

    public void OnMousePressed()
    {
        print("touched " + blockId);
        //GetComponent<Image>().color = lightColor;
    }
}
