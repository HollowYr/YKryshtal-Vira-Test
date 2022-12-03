using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : Singleton<MainUI>
{
    [SerializeField] internal ScoreUI scoreUI;
    [SerializeField] internal FailUI failUI;
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
        //Enable failUI
        failUI.Show();
    }
}
