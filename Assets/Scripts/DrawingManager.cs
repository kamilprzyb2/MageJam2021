using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingManager : MonoBehaviour
{
    public GameObject lineTemplate;
    public float zAdjustment = 10f;
    public float lineInterval = 0.01f;
    public Transform topRightCorner;
    public Transform bottomLeftCorner;
    public Camera ortoCamera;

    private Line activeLine;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = ortoCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));

            if (mousePos.x > topRightCorner.position.x || mousePos.x < bottomLeftCorner.position.x ||
                mousePos.y > topRightCorner.position.y || mousePos.y < bottomLeftCorner.position.y)
            {
                activeLine = null;
            }
            else if (activeLine == null)
            {
                GameObject newLine = Instantiate(lineTemplate);
                activeLine = newLine.GetComponent<Line>();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            activeLine = null;
        }
        

        if (activeLine != null)
        {
            Vector2 mousePos = ortoCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
            activeLine.UpdateLine(mousePos, zAdjustment, lineInterval);
        }

        

    }

}
