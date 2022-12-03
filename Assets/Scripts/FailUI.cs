using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class FailUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highestScore;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button settingsButton;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        MainUI.Instance.failUI = this;
    }
    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        restartButton.onClick.AddListener(Restart);
        //settingsButton.onClick.AddListener();
        Hide();
    }

    private void Restart()
    {
        GameController.Instance.ReloadScene();
    }

    public void Show()
    {
        highestScore.text = GameData.Instance.GetHighestScore().ToString();
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
    }
    public void Hide()
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
    }
}
