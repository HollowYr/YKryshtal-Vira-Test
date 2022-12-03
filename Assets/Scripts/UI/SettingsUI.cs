using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsUI : UI
{
    public override void Start()
    {
        base.Start();
        MainUI.Instance.settingsUI = this;
        Hide();
    }

    public override void Show()
    {
        base.Show();
        GameController.Instance.isPlayable = false;
    }
}
