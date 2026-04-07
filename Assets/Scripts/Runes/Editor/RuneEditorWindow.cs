using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RuneEditorWindow : EditorWindow
{
    private List<Vector2> drawnPoints = new List<Vector2>();
    private List<Vector2> turnPositions = new List<Vector2>();
    private List<float> turnAngles = new List<float>();
    private bool isDrawing = false;

    private string runeName = "New Rune";
    private RuneData existingRune;
    private float turnThreshold = 30f;

    private const float canvasSize = 400f;
    private Rect canvasRect;

    [MenuItem("Tools/Rune Editor")]
    public static void ShowWindow()
    {
        GetWindow<RuneEditorWindow>("Rune Editor");
    }

    private void OnGUI()
    {
        GUILayout.Label("Rune Template Editor", EditorStyles.boldLabel);

        runeName = EditorGUILayout.TextField("Rune Name", runeName);
        existingRune = (RuneData)EditorGUILayout.ObjectField("Overwrite Existing Rune", existingRune, typeof(RuneData), false);
        turnThreshold = EditorGUILayout.Slider("Turn Threshold", turnThreshold, 15f, 60f);

        GUILayout.Space(10);

        canvasRect = GUILayoutUtility.GetRect(canvasSize, canvasSize);
        EditorGUI.DrawRect(canvasRect, Color.black);

        DrawStoredPoints();
        HandleCanvasInput(Event.current);

        GUILayout.Space(10);

        GUILayout.Label($"Points: {drawnPoints.Count} | Turns: {turnAngles.Count}");

        if (turnAngles.Count > 0)
        {
            string angles = "Angles: ";
            foreach (var angle in turnAngles)
                angles += $"{angle:F0}° ";
            GUILayout.Label(angles);
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Clear"))
        {
            drawnPoints.Clear();
            turnPositions.Clear();
            turnAngles.Clear();
        }

        if (GUILayout.Button("Save"))
        {
            SaveRune();
        }

        Repaint();
    }

    private void HandleCanvasInput(Event e)
    {
        if (!canvasRect.Contains(e.mousePosition))
            return;

        if (e.type == EventType.MouseDown && e.button == 0)
        {
            isDrawing = true;
            drawnPoints.Clear();
            turnPositions.Clear();
            turnAngles.Clear();
            AddPoint(e.mousePosition);
            e.Use();
        }

        if (e.type == EventType.MouseDrag && isDrawing)
        {
            AddPoint(e.mousePosition);
            e.Use();
        }

        if (e.type == EventType.MouseUp && e.button == 0)
        {
            isDrawing = false;
            e.Use();
        }
    }

    private void AddPoint(Vector2 mousePos)
    {
        Vector2 localPoint = mousePos - canvasRect.position;

        if (drawnPoints.Count > 0 && Vector2.Distance(drawnPoints[drawnPoints.Count - 1], localPoint) < 5f)
            return;

        drawnPoints.Add(localPoint);
        DetectTurn();
    }

    private void DetectTurn()
    {
        if (drawnPoints.Count < 3)
            return;

        int last = drawnPoints.Count - 1;
        Vector2 prevDir = (drawnPoints[last - 1] - drawnPoints[last - 2]).normalized;
        Vector2 currDir = (drawnPoints[last] - drawnPoints[last - 1]).normalized;

        float angle = Vector2.SignedAngle(prevDir, currDir);

        if (Mathf.Abs(angle) >= turnThreshold)
        {
            turnPositions.Add(drawnPoints[last - 1]);
            turnAngles.Add(angle);
        }
    }

    private void DrawStoredPoints()
    {
        if (drawnPoints.Count < 2)
            return;

        Handles.color = Color.white;
        for (int i = 0; i < drawnPoints.Count - 1; i++)
        {
            Vector2 p1 = canvasRect.position + drawnPoints[i];
            Vector2 p2 = canvasRect.position + drawnPoints[i + 1];
            Handles.DrawLine(p1, p2);
        }

        // Draw turn points
        Handles.color = Color.yellow;
        foreach (var pos in turnPositions)
        {
            Handles.DrawSolidDisc(canvasRect.position + pos, Vector3.forward, 4f);
        }
    }

    private void SaveRune()
    {
        if (turnAngles.Count < 2)
        {
            Debug.LogWarning("Need at least 2 turns to save a rune");
            return;
        }

        RuneData runeToSave = existingRune;

        if (runeToSave == null)
        {
            runeToSave = ScriptableObject.CreateInstance<RuneData>();
            runeToSave.runeName = runeName;

            string path = EditorUtility.SaveFilePanelInProject("Save Rune", runeName, "asset", "Choose where to save");

            if (string.IsNullOrEmpty(path))
                return;

            AssetDatabase.CreateAsset(runeToSave, path);
        }

        runeToSave.runeName = runeName;
        runeToSave.turnAngles = new List<float>(turnAngles);

        EditorUtility.SetDirty(runeToSave);
        AssetDatabase.SaveAssets();

        Debug.Log($"Saved rune '{runeName}' with {turnAngles.Count} turns");
    }
}
