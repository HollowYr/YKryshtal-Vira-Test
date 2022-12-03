using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class EnableSettingsUIButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ShowSettingsUI);
    }

    private void ShowSettingsUI()
    {
        MainUI.Instance.settingsUI.Show();
        MainUI.Instance.scoreUI.Hide();
        MainUI.Instance.startUI.Hide();
    }
}
