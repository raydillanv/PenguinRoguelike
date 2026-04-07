using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRune", menuName = "Runes/Rune Data")]
public class RuneData : ScriptableObject
{
    public string runeName;
    public List<float> turnAngles = new List<float>();

    [Header("Spell Info")]
    public GameObject spellPrefab;
}
