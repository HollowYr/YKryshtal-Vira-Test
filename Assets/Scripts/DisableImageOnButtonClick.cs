using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableImageOnButtonClick : MonoBehaviour
{
    [SerializeField] private Image image;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(SetActive);
        SetActive();
    }

    private void SetActive()
    {
        this.DoAfterNextFrame(() =>
        image.gameObject.SetActive(GameData.Instance.IsLightTheme()));
    }
}
