using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameMode {DRAWING, MOVING}

public class DrawingManager : MonoBehaviour
{
    public bool disableDrawing = false;
    public float maxLength = 10f;
    public GameMode gameMode = GameMode.DRAWING;
    public GameObject lineTemplate;
    public float zAdjustment = 10f;
    public float lineInterval = 0.01f;
    public Box drawArea;
    public List<Box> preventFromDrawing;
    public Camera ortoCamera;
    public Image chalkbar;

    private float chalkBarDelta;
    private Line activeLine;
    private Stack<Line> lines;
    [SerializeField]
    private float remainingLength;

    private void Start()
    {
        lines = new Stack<Line>();
        if (disableDrawing)
            gameMode = GameMode.MOVING;
        remainingLength = maxLength;

        if (chalkbar)
        chalkBarDelta = chalkbar.rectTransform.sizeDelta.x;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !disableDrawing)
        {
            gameMode = gameMode == GameMode.DRAWING ? GameMode.MOVING : GameMode.DRAWING;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (lines.Count > 0)
            {
                Line toRemove = lines.Pop();
                remainingLength += toRemove.length;
                Destroy(toRemove.gameObject);

                UpdateChalkBar();
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }


        // if player holds left mouse button
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = ortoCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));

            float length = LineLength(mousePos);

            if (IsAbleToDraw(mousePos) && length <= remainingLength)
            {
                if (activeLine == null)
                {
                    StartNewLine(mousePos);
                }
                else if (IsFarAwayFromLastPoint(mousePos))
                {
                    activeLine.UpdateLine(mousePos, zAdjustment);
                    UpdateChalkBar();
                }
                activeLine.length += length;
                remainingLength -= length;
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

    private float LineLength(Vector2 mousePos)
    {
        if (activeLine == null || activeLine.points.Count == 0)
            return 0;

        Vector2 lastPoint = activeLine.points[activeLine.points.Count - 1];
        return (lastPoint - mousePos).sqrMagnitude;
    }
    private void StartNewLine(Vector2 mousePos)
    {
        GameObject newLine = Instantiate(lineTemplate);
        activeLine = newLine.GetComponent<Line>();
        activeLine.UpdateLine(mousePos, zAdjustment);
    }
    private void EndLine()
    {
        if (activeLine.points.Count < 2)
        {
            Destroy(activeLine.gameObject);
        }
        else
        {
            lines.Push(activeLine);
        }   
        activeLine = null;
    }

    private void UpdateChalkBar()
    {
        float value = remainingLength / maxLength;
        chalkbar.rectTransform.sizeDelta = new Vector2(chalkBarDelta * value, chalkbar.rectTransform.sizeDelta.y);
    }
}
