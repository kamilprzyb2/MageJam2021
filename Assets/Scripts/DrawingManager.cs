using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum GameMode {DRAWING, MOVING}

public class DrawingManager : MonoBehaviour
{
    public GameMode gameMode = GameMode.DRAWING;
    public GameObject lineTemplate;
    public float zAdjustment = 10f;
    public float lineInterval = 0.01f;
    public Box drawArea;
    public List<Box> preventFromDrawing;
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

        // if player holds left mouse button
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = ortoCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));

            if (IsAbleToDraw(mousePos))
            {
                if (activeLine == null)
                {
                    StartNewLine(mousePos);
                }
                else if (IsFarAwayFromLastPoint(mousePos))
                {
                    activeLine.UpdateLine(mousePos, zAdjustment);
                }
            }
            else if (activeLine != null)
            {
                EndLine();
            }
        }
        else if (activeLine != null)
        {
            EndLine();
        }

    }

    private bool IsAbleToDraw(Vector2 mousePos)
    {
        if (gameMode != GameMode.DRAWING || !drawArea.IsInBox(mousePos))
        {
            return false;
        }
        foreach (Box box in preventFromDrawing)
        {
            if (box.IsInBox(mousePos))
            {
                return false;
            }
        }
        return true;
    }
    private bool IsFarAwayFromLastPoint(Vector2 mousePos)
    {
        if (activeLine == null || activeLine.points.Count == 0)
        {
            return true;
        }
        return Vector2.Distance(activeLine.points.Last(), mousePos) > lineInterval;
    }
    private void StartNewLine(Vector2 mousePos)
    {
        GameObject newLine = Instantiate(lineTemplate);
        activeLine = newLine.GetComponent<Line>();
        activeLine.UpdateLine(mousePos, zAdjustment);
    }
    private void EndLine()
    {
        lines.Push(activeLine);
        activeLine = null;
    }
}
