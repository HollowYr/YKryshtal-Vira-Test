using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(CanvasGroup))]
public class ScoreUI : UI
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI starsText;
    private void Awake()
    {
        MainUI.Instance.scoreUI = this;
        SetScoreText(0);
    }

    public override void Start()
    {
        base.Start();
        Show();
    }

    public override void Show()
    {
        if (canvasGroup.alpha > 0) return;
        base.Show();

        //if (GameController.Instance.isPlayable != false) return;
        GameController.Instance.isPlayable = true;
        MainUI.Instance.startUI.Show();
        GameController.Instance.ChangeBasketsSortingLayer(0);
    }

    public override void Hide()
    {
        base.Hide();
        GameController.Instance.ChangeBasketsSortingLayer(-1);

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
