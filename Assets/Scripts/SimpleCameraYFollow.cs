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
        GameController.Instance.onNewBasketScored += Instance_onNewBasketScored;
        Application.quitting += Application_quitting;
    }

    private void Application_quitting()
    {
        GameController.Instance.onNewBasketScored -= Instance_onNewBasketScored;
    }

    private void Instance_onNewBasketScored(Transform transform)
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
