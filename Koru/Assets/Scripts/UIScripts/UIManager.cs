using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TextMeshProUGUI winnerText;

    public void OpenGameOverPanel(char team)
    {
        gameOverPanel.SetActive(true);

        if (team == 'w')
        {
            winnerText.text = "Player White Won!";
        }
        else
        {
            winnerText.text = "Player Black Won!";
        }
    }

    public void ExitButton()
    {
        print("exit ehe");
    }
}
