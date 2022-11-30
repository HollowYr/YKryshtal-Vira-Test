using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    float distance, screenHeight;
    Vector2 startPos, endPos;
    // Start is called before the first frame update
    void Start()
    {
        screenHeight = Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            endPos = Input.mousePosition;
            distance = Vector2.Distance(startPos, endPos);
            //Debug.Log(/*"Start: " + startPos + " End: " + endPos + */" Distance: " + distance + " Screen%: " + (distance / screenHeight) * 100f);
        }
    }
}
