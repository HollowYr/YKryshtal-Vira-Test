using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButtonSpriteOnClick : MonoBehaviour
{
    [SerializeField] private Sprite activated;
    [SerializeField] private Sprite deactivated;
    [SerializeField] private Image imageToChange;
    [SerializeField] private bool useTheme = false;
    bool isLight = true;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ChangeSprite);
        ChangeSprite();
    }

    private void ChangeSprite()
    {
        this.DoAfterNextFrame(() =>
        {
            bool lightOrDark = (useTheme) ? GameData.Instance.IsLightTheme() : isLight;

            if (lightOrDark)
            {
                imageToChange.sprite = activated;
                isLight = false;
            }
            else
            {
                isLight = true;
                imageToChange.sprite = deactivated;
            }
        });
    }
}
