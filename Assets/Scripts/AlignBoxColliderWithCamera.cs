using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class AlignBoxColliderWithCamera : MonoBehaviour
{
    private Camera camera;
    private BoxCollider2D boxCollider;
    private float sizeX, sizeY, aspectRatio;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        camera = GetComponent<Camera>();

        sizeY = camera.orthographicSize * 2;
        aspectRatio = (float)Screen.width / (float)Screen.height;
        sizeX = sizeY * aspectRatio;
        boxCollider.size = new Vector2(sizeX, sizeY);
    }
}
