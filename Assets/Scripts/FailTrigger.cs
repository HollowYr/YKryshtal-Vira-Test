using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out BallCollision ball)) return;
        ball.gameObject.SetActive(false);
        //Debug.Log("fail Collision: " + ball.transform.name);
        GameController.Instance.InvokeOnFail();
    }
}
