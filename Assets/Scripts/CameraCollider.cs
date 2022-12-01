using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[RequireComponent(typeof(EdgeCollider2D))]
public class CameraCollider : MonoBehaviour
{
    private const float ballDiameter = 1;
    EdgeCollider2D edgeCollider;
    private Vector2[] edges = new Vector2[4];
    private float sizeX, sizeY, aspectRatio;
    private Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();

        sizeY = camera.orthographicSize * 2;
        aspectRatio = (float)Screen.width / (float)Screen.height;
        sizeX = sizeY * aspectRatio;


        edges[0] = new Vector2(-sizeX / 2, (-sizeY / 2) - ballDiameter);
        edges[1] = new Vector2(-sizeX / 2, sizeY / 2);
        edges[2] = new Vector2(sizeX / 2, sizeY / 2);
        edges[3] = new Vector2(sizeX / 2, (-sizeY / 2) - ballDiameter);
        //edges[4] = new Vector2(-sizeX / 2, -sizeY / 2);


        edgeCollider = GetComponent<EdgeCollider2D>();
        edgeCollider.points = edges;
    }
}
