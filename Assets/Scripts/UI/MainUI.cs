using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : Singleton<MainUI>
{
    [SerializeField] internal ScoreUI scoreUI;
    [SerializeField] internal FailUI failUI;
    [SerializeField] internal SettingsUI settingsUI;
    [SerializeField] internal StartUI startUI;
    [SerializeField] internal PauseUI pauseUI;
    internal UI currentUI;
    internal UI previousUI;

    public void SetCurrentUI(UI currentUI)
    {
        GameController.Instance.isPlayable = (currentUI == scoreUI || currentUI == startUI) ? true : false;
        this.currentUI = currentUI;
    }

    public void ShowPreviousUI()
    {
        UI currentUI = this.currentUI;
        UI previousUI = this.previousUI;
        currentUI.Hide();
        previousUI.Show();
    }

    public void Start()
    {
        GameController.Instance.OnFail += Instance_OnFail;
        Application.quitting += Application_quitting;
    }

    private void Application_quitting()
    {
        GameController.Instance.OnFail -= Instance_OnFail;
    }

    private void Instance_OnFail()
    {
        failUI.Show();
    }
}
