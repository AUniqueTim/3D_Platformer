using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    public int points;
    [SerializeField] private int winThreshold;

    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Update()
    {
        scoreText.text = points.ToString();

        if (points >= winThreshold)
        {
            winPanel.SetActive(true);
        }
        else { winPanel.SetActive(false); }
    }
}
