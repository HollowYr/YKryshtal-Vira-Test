using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundUIObserver : MonoBehaviour
{

    Image background;
    private static Color32 DARK_THEME = new Color(.248f, .248f, .248f, 1f);
    private static Color32 LIGHT_THEME = new Color(0.9098039f, 0.9098039f, 0.9098039f, 1f);


    void Start()
    {
        background = GetComponent<Image>();
        GameController.Instance.OnBackgroundChange += ChangeBackground;
        Application.quitting += Application_quitting;
    }

    private void Application_quitting()
    {
        GameController.Instance.OnBackgroundChange -= ChangeBackground;
    }

    private void ChangeBackground(bool isLightTheme)
    {
        if (isLightTheme)
        {
            background.color = LIGHT_THEME;
        }
        else
        {
            background.color = DARK_THEME;
        }

    }
}
