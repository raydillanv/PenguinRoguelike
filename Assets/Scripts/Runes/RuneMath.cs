using System.Collections.Generic;
using UnityEngine;

public static class RuneMath
{
    // public static bool CompareAngles(List<TurnPoint> input, RuneData rune, float tolerance)
    // {
    //     if (input.Count != rune.turnAngles.Count)
    //         return false;
    //
    //     // Try forward
    //     if (MatchDirection(input, rune.turnAngles, tolerance, false))
    //         return true;
    //
    //     // Try reversed
    //     if (MatchDirection(input, rune.turnAngles, tolerance, true))
    //         return true;
    //
    //     return false;
    // }
    //
    // private static bool MatchDirection(List<TurnPoint> input, List<float> template, float tolerance, bool reversed)
    // {
    //     int count = input.Count;
    //
    //     for (int i = 0; i < count; i++)
    //     {
    //         int idx = reversed ? (count - 1 - i) : i;
    //         float inputAngle = reversed ? -input[idx].turnAngle : input[idx].turnAngle;
    //         float templateAngle = template[i];
    //
    //         if (Mathf.Abs(Mathf.DeltaAngle(inputAngle, templateAngle)) > tolerance)
    //             return false;
    //     }
    //
    //     return true;
    // }
}
