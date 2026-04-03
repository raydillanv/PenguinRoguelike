using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRune", menuName = "Runes/Rune Data")]
public class RuneData : ScriptableObject
{
    public string runeName;
    
    [Tooltip("Normalized template of points.")]
    public List<Vector2> points = new List<Vector2>();
    
    [Header("Spell Info")]
    public GameObject spellPrefab;
    
}
