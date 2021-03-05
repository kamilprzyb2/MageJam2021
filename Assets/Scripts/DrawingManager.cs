using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingManager : MonoBehaviour
{
    public GameObject lineTemplate;

    private Line activeLine;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (activeLine == null)
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
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
            activeLine.UpdateLine(mousePos);
        }

        

    }

}
