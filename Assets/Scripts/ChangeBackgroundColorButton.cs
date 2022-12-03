using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ChangeBackgroundColorButton : MonoBehaviour
{
    //[SerializeField] private Image backgroundImage;

    //private static Color32 DARK_THEME = new Color(.248f, .248f, .248f, 1f);
    //private static Color32 LIGHT_THEME = new Color(0.9098039f, 0.9098039f, 0.9098039f, 1f);

    Button button;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ChangeBackground);
    }

    private void ChangeBackground()
    {
        bool isLightTheme = GameData.Instance.IsLightTheme();
        isLightTheme = !isLightTheme;
        GameData.Instance.SetThemeColor(isLightTheme);
    }
}
