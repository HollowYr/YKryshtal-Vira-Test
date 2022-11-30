using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{

    public static Vector3 Clamp(this Vector3 v, float min, float max) =>
        new Vector3(Mathf.Clamp(v.x, min, max), Mathf.Clamp(v.y, min, max), Mathf.Clamp(v.z, min, max));

    public static Vector3 Abs(this Vector3 v) => new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
    public static float GetValueByDirection(this Vector3 v, Vector3 dir)
    {
        if (dir.x != 0) return v.x;
        if (dir.y != 0) return v.y;
        if (dir.z != 0) return v.z;
        return 0;
    }

    private static IEnumerator DoAfterTime(float duration, Action OnComplete)
    {
        yield return new WaitForSeconds(duration);
        OnComplete?.Invoke();
    }

    public static void DoAfterNextFrame(this MonoBehaviour monoBehaviour, Action onComplete)
    {
        monoBehaviour.StartCoroutine(DoAfterTime(0, onComplete));
    }

    public static void DoAfterTime(this MonoBehaviour monoBehaviour, float waitForTime, Action onComplete)
    {
        monoBehaviour.StartCoroutine(DoAfterTime(waitForTime, onComplete));
    }

}
