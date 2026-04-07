using System.Collections.Generic;
using UnityEngine;

public class RuneDetector : MonoBehaviour
{
    public RuneLibrary runeLibrary;
    public SpellCaster spellCaster;

    [Header("Recognition Settings")]
    public float angleTolerance = 25f;
    public int maxRuneSize = 6;

    // public RuneData TryDetectRune(List<TurnPoint> turnPoints)
    // {
    //     if (turnPoints == null || turnPoints.Count < 2)
    //         return null;
    //
    //     if (runeLibrary == null || runeLibrary.runes.Count == 0)
    //     {
    //         Debug.LogWarning("RuneDetector: No rune library or empty library!");
    //         return null;
    //     }
    //
    //     // DEBUG: Build input angles string for debug
    //     string inputAngles = "";
    //     foreach (var tp in turnPoints)
    //         inputAngles += $"{tp.turnAngle:F0}° ";
    //     Debug.Log($"Checking {turnPoints.Count} turns: [{inputAngles}]");
    //
    //     foreach (var rune in runeLibrary.runes)
    //     {
    //         if (!rune || rune.turnAngles.Count == 0) continue;
    //
    //         if (turnPoints.Count < rune.turnAngles.Count) continue;
    //
    //         // Build template angles string for debug
    //         string templateAngles = "";
    //         foreach (var a in rune.turnAngles)
    //             templateAngles += $"{a:F0}° ";
    //
    //         // Sliding window - check each window state
    //         for (int start = 0; start <= turnPoints.Count - rune.turnAngles.Count; start++)
    //         {
    //             var window = turnPoints.GetRange(start, rune.turnAngles.Count);
    //
    //             string windowAngles = "";
    //             foreach (var tp in window)
    //                 windowAngles += $"{tp.turnAngle:F0}° ";
    //
    //             Debug.Log($"Comparing window [{windowAngles}] vs {rune.runeName} [{templateAngles}]");
    //
    //             if (RuneMath.CompareAngles(window, rune, angleTolerance))
    //             {
    //                 Debug.Log($"<color=green>MATCHED rune: {rune.runeName}</color>");
    //                 if (spellCaster) spellCaster.castRune(rune);
    //                 return rune;
    //             }
    //         }
    //     }
    //
    //     return null;
    // }
}
