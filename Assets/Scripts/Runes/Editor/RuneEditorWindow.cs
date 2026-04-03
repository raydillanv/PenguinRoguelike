using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RuneEditorWindow : EditorWindow
{
    private List<Vector2> drawnPoints = new List<Vector2>();
    private bool isDrawing = false;
    
    private string runeName = "New Rune";
    private RuneData existingRune;

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
        
        GUILayout.Space(10);
        
        canvasRect = GUILayoutUtility.GetRect(canvasSize, canvasSize);
        EditorGUI.DrawRect(canvasRect, Color.black);

        drawStoredPoints();
        handleCanvasInput(Event.current);
        
        GUILayout.Space(10);

        if (GUILayout.Button("Clear"))
        {
            drawnPoints.Clear();
        }

        if (GUILayout.Button("Save"))
        {
            saveRune();
        }
        
        Repaint();
    }

    private void handleCanvasInput(Event e)
    {
        if (!canvasRect.Contains(e.mousePosition))
        {
            return;
        }

        if (e.type == EventType.MouseDown && e.button == 0)
        {
            isDrawing = true;
            drawnPoints.Clear();
            addPoint(e.mousePosition);
            e.Use();
        }

        if (e.type == EventType.MouseUp && isDrawing)
        {
            addPoint(e.mousePosition);
            e.Use();
        }
        
        if (e.type == EventType.MouseUp && e.button == 0)
        {
            isDrawing = false;
            e.Use();
        }
    }

    private void addPoint(Vector2 mousePos)
    {
        Vector2 localPoint = mousePos - canvasRect.position;

        if (drawnPoints.Count == 0 || Vector2.Distance(drawnPoints[drawnPoints.Count - 1], localPoint) > 5f)
        {
            drawnPoints.Add(localPoint);
        }
        
    }

    private void drawStoredPoints()
    {
        if (drawnPoints.Count < 2)
        {
            return;
        }
        
        Handles.color = Color.aquamarine;

        for (int i = 0; i < drawnPoints.Count - 1; i++)
        {
            Vector2 p1 = canvasRect.position + drawnPoints[i];
            Vector2 p2 = canvasRect.position + drawnPoints[i + 1];
            Handles.DrawLine(p1, p2);
        }
    }

    private void saveRune()
    {
        if (drawnPoints.Count < 2)
        {
            Debug.LogWarning("Not enough points bro");
            return;
        }
        
        List<Vector2> normalizedPoints = RuneMath.normalize(drawnPoints);

        RuneData runeToSave = existingRune;

        if (runeToSave == null)
        {
            runeToSave = ScriptableObject.CreateInstance<RuneData>();
            runeToSave.runeName = runeName;

            string path = EditorUtility.SaveFilePanelInProject("Save Rune", runeName + "asset", "asset",
                "Choose where to save it");

            if (string.IsNullOrEmpty(path))
            {
                return;
            }
            
            AssetDatabase.CreateAsset(runeToSave, path);

        }
        
        runeToSave.runeName = runeName;
        runeToSave.points = normalizedPoints;
        
        EditorUtility.SetDirty(runeToSave);
        AssetDatabase.SaveAssets();

    }
}
