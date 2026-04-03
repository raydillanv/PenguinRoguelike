using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RuneLibrary", menuName = "Runes/Rune Library")]
public class RuneLibrary : ScriptableObject
{
    public List<RuneData>  runes = new List<RuneData>();
}
