using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    private void Start()
    {
        TrajectoryPrediction.Instance.SetBall(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.transform.name);
    }

}
