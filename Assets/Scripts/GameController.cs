using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    [SerializeField] private Transform startBasket;
    public event Action<Transform> OnNewBasketScored;

    private int lastBasketHashcode = 0;
    public void InvokeOnNewBasketScored(Transform transform)
    {
        if (transform.GetHashCode() == lastBasketHashcode || lastBasketHashcode == 0) return;
        lastBasketHashcode = transform.GetHashCode();
        OnNewBasketScored?.Invoke(transform);
    }

    private void Start()
    {
        lastBasketHashcode = startBasket.GetHashCode();
    }
}
