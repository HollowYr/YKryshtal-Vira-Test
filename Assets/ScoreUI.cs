using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI starsText;
    public void SetScoreText(int score)
    {
        scoreText.text = score.ToString();
    }
    public void ChangeStarText(int score)
    {
        scoreText.text = score.ToString();
    }
}
