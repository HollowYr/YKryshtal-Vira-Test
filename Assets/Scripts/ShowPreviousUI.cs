using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShowPreviousUI : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ShowPrevious);
    }

    private void ShowPrevious()
    {
        MainUI.Instance.ShowPreviousUI();
    }


}
