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
    [SerializeField] private int wacManPointsThreshold;
    public int wacManPoints;
    [SerializeField] private TextMeshProUGUI wacManPointsText;

    private void Update()
    {
        scoreText.text = points.ToString();
        wacManPointsText.text = wacManPoints.ToString();

        if (points >= winThreshold)
        {
            winPanel.SetActive(true);
        }
        else { winPanel.SetActive(false); }

        if (wacManPoints >= wacManPointsThreshold)
        {
            losePanel.SetActive(true);
        }
        else { losePanel.SetActive(false); }
    }
}
