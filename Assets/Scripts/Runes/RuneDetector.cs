using System.Collections.Generic;
using UnityEngine;

public class RuneDetector : MonoBehaviour
{
    public RuneLibrary runeLibrary;
    public SpellCaster spellCaster;
    
    [Header("Recognition Settings")]
    [Tooltip("Lower = stricter, Higher = more forgiving")]
    public float matchThreshold = 0.30f;
    
    public bool tryDetectRune(List<Vector2> drawnPoints)
    {
        if (drawnPoints == null || drawnPoints.Count < 8)
        {
            return false;
        }

        List<Vector2> normalizedInput = RuneMath.normalize(drawnPoints);

        RuneData bestMatch = null;
        float bestScore = float.MaxValue;

        foreach (RuneData rune in runeLibrary.runes)
        {
            if (rune == null || rune.points == null || rune.points.Count == 0)
                continue;

            float score = RuneMath.comparePointLists(normalizedInput, rune.points);

            Debug.Log($"Comparing to {rune.runeName}: score = {score}");

            if (score < bestScore)
            {
                bestScore = score;
                bestMatch = rune;
            }
        }

        if (bestMatch != null && bestScore <= matchThreshold)
        {
            Debug.Log($"Matched rune: {bestMatch.runeName} (score: {bestScore})");

            if (spellCaster != null)
            {
                spellCaster.castRune(bestMatch);
            }

            return true;
        }

        return false;
    }
    
}
