using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI starsText;

    private void Awake()
    {
        MainUI.Instance.scoreUI = this;
        SetScoreText(0);
    }

    public void SetScoreText(int score)
    {
        scoreText.text = score.ToString();
    }
    public void SetStarText(int score)
    {
        starsText.text = score.ToString();
    }
}
