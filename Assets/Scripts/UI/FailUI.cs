using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class FailUI : UI
{
    [SerializeField] private TextMeshProUGUI highestScore;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button settingsButton;

    private void Awake()
    {
        MainUI.Instance.failUI = this;
    }
    public override void Start()
    {
        base.Start();
        restartButton.onClick.AddListener(Restart);
        Hide();
    }

    private void Restart()
    {
        GameController.Instance.ReloadScene();
    }

    public override void Show()
    {
        highestScore.text = GameData.Instance.GetHighestScore().ToString();
        base.Show();
    }
}
