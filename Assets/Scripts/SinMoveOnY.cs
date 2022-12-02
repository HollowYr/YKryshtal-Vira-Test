using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinMoveOnY : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] float height;

    Tween moveTween;
    private void Start()
    {
        StartTween();
    }

    public void StartTween()
    {
        moveTween = transform.DOLocalMoveY(transform.localPosition.y + height, duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    public void KillTween() => moveTween.Kill();

    private void OnDestroy()
    {
        KillTween();
    }
}
