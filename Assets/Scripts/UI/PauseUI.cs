using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : UI
{
    [SerializeField] private Button resumeButton;
    // Update is called once per frame
    public override void Start()
    {
        base.Start();
        MainUI.Instance.pauseUI = this;
        resumeButton.onClick.AddListener(() => MainUI.Instance.ShowPreviousUI());
        Hide();
    }
}
