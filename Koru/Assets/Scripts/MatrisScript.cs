using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatrisScript : MonoBehaviour
{
    [SerializeField] private GameObject block;
    [SerializeField] private Color color;

    private int width = 9;
    void Start()
    {
        SetTheMatris();
    }

    void Update()
    {

    }

    private void SetTheMatris()
    {
        for (int i = 0; i < width * width; i++)
        {
            if (i % 2 == 0)
            {
                Instantiate(block, transform).GetComponent<Image>().color = color;
            }
            else
            {
                Instantiate(block, transform);
            }
        }
    }
}
