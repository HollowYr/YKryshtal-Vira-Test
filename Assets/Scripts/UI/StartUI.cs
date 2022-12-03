using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : UI
{
    //[SerializeField] private TextMeshProUGUI scoreText;
    //[SerializeField] private TextMeshProUGUI starsText;

    [SerializeField] private Button pauseButton;

    private void Awake()
    {
        MainUI.Instance.startUI = this;

    }
    public override void Start()
    {
        base.Start();
        Show();
        pauseButton.onClick.AddListener(EnablePause);
        MainUI.Instance.currentUI = this;
        //SetScoreText(0);
    }

    private void EnablePause()
    {
        MainUI.Instance.pauseUI.Show();
        MainUI.Instance.previousUI = this;
        Hide();
    }

    public override void Show()
    {
        if (canvasGroup.alpha > 0) return;
        base.Show();
        MainUI.Instance.scoreUI.Show();
    }

    public override void Hide()
    {
        base.Hide();
        MainUI.Instance.scoreUI.Hide();
    }
    //public void SetScoreText(int score)
    //{
    //    scoreText.text = score.ToString();
    //}
    //public void SetStarText(int score)
    //{
    //    starsText.text = score.ToString();
    //}
}
