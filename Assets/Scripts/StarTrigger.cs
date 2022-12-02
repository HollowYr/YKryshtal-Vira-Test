using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.transform.TryGetComponent(out BallCollision ball)) return;
        GameData.Instance.AddStar(1);
        Destroy(gameObject);
    }
}
