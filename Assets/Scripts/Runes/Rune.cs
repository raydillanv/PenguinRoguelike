using System.Collections.Generic;
using UnityEngine;

namespace Runes
{
    [CreateAssetMenu(fileName = "New Rune", menuName = "Runes/Rune")]
    public class Rune : ScriptableObject
    {
        public List<Vector2> directions = new List<Vector2>();
        public GameObject runePrefab;
        public float cooldown = 1f;

        public int Size => directions.Count;
        public Vector2[] Vectors => directions.ToArray();

        private void OnValidate()
        {
            for (int i = 0; i < directions.Count; i++)
                directions[i] = directions[i].normalized;
        }
    }
}
