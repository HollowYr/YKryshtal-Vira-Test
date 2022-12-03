using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CanvasGroup))]
public class UI : MonoBehaviour
{
    protected CanvasGroup canvasGroup;
    public virtual void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public virtual void Show()
    {
        MainUI.Instance.currentUI = this;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
    }
    public virtual void Hide()
    {
        MainUI.Instance.previousUI = this;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
    }
}
