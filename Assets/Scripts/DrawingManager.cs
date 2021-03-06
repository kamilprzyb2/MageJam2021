using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode {DRAWING, MOVING}

public class DrawingManager : MonoBehaviour
{
    public GameMode gameMode = GameMode.DRAWING;
    public GameObject lineTemplate;
    public float zAdjustment = 10f;
    public float lineInterval = 0.01f;
    public Transform topRightCorner;
    public Transform bottomLeftCorner;
    public Camera ortoCamera;


    private Line activeLine;
    private Stack<Line> lines;

    private void Start()
    {
        lines = new Stack<Line>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            gameMode = gameMode == GameMode.DRAWING ? GameMode.MOVING : GameMode.DRAWING;
        }
        if (Input.GetKeyDown(KeyCode.Z) && gameMode == GameMode.DRAWING)
        {
            if (lines.Count > 0)
            {
                Line toRemove = lines.Pop();
                Destroy(toRemove.gameObject);
            }
        }


        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = ortoCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));

            if (gameMode == GameMode.MOVING ||
                mousePos.x > topRightCorner.position.x || mousePos.x < bottomLeftCorner.position.x ||
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
            lines.Push(activeLine);
            activeLine = null;
        }
        

        if (activeLine != null)
        {
            Vector2 mousePos = ortoCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
            activeLine.UpdateLine(mousePos, zAdjustment, lineInterval);
        }

        

    }

}
