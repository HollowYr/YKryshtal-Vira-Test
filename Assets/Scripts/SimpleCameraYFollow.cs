using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraYFollow : MonoBehaviour
{
    [SerializeField] Transform followTarget;
    [SerializeField] float smoothTime = 0.5f;
    float velocity;
    Vector3 previousLowestPoint;
    void Start()
    {
        previousLowestPoint = transform.position;
        GameController.Instance.OnNewBasketScored += Instance_OnNewBasketScored;
        Application.quitting += Application_quitting;
    }

    private void Application_quitting()
    {
        GameController.Instance.OnNewBasketScored -= Instance_OnNewBasketScored;
    }

    private void Instance_OnNewBasketScored(Transform transform)
    {
        previousLowestPoint = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (previousLowestPoint.y >= followTarget.position.y) return;
        Vector3 position = transform.position;
        position.y = Mathf.SmoothDamp(position.y, followTarget.position.y, ref velocity, smoothTime);
        transform.position = position;
    }
}
