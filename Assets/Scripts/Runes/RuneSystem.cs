using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Runes
{
    public class RuneSystem : MonoBehaviour
    {
        public GameObject playerReference;

        public List<Rune> runes = new List<Rune>();
        private Dictionary<Rune, int> activeMatches = new Dictionary<Rune, int>();
        private List<Rune> unactiveRunes = new List<Rune>();

        public float matchThreshold = 0.85f;
        public bool debug;

        public void Start()
        {
            activeMatches = new Dictionary<Rune, int>();
            unactiveRunes = new List<Rune>(runes);
        }

        public void NextDirection(Vector2 direction)
        {
            if (direction.sqrMagnitude < 0.001f) return;
            direction = direction.normalized;

            if (debug) Debug.Log($"[Rune] Direction: {direction}");

            // Check existing matches and update/remove
            var toRemove = new List<Rune>();
            var toUpdate = new List<(Rune rune, int count)>();
            var completed = new List<Rune>();

            foreach (var (rune, matchedCount) in activeMatches)
            {
                float dot = Vector2.Dot(direction, rune.Vectors[matchedCount]);

                if (dot >= matchThreshold)
                {
                    int newCount = matchedCount + 1;
                    toUpdate.Add((rune, newCount));
                    if (debug) Debug.Log($"[Rune] {rune.name}: {newCount}/{rune.Size}");
                    if (newCount >= rune.Size) completed.Add(rune);
                } else {
                    if (debug) Debug.Log($"[Rune] {rune.name} failed (dot: {dot:F2})");
                    toRemove.Add(rune);
                }
            }

            // Apply updates
            foreach (var (rune, count) in toUpdate)
                activeMatches[rune] = count;

            // Handle failed matches
            foreach (var rune in toRemove)
            {
                activeMatches.Remove(rune);
                unactiveRunes.Add(rune);
            }

            // Handle completed runes
            foreach (var rune in completed)
            {
                OnRuneComplete(rune);
                activeMatches.Remove(rune);
                unactiveRunes.Add(rune);
            }

            // Handle new direction matches
            var newRunes = new List<Rune>();
            foreach (var rune in unactiveRunes.Where(rune => Vector2.Dot(direction, rune.Vectors[0]) >= matchThreshold))
            {
                activeMatches[rune] = 1;
                if (debug) Debug.Log($"[Rune] Started: {rune.name}");
                newRunes.Add(rune);
            }

            // Update unactive tracker
            foreach (var rune in newRunes) unactiveRunes.Remove(rune);
        }

        private void OnRuneComplete(Rune rune)
        {
            Debug.Log($"Rune matched: {rune.name}");
            if (rune.runePrefab && playerReference)
                Instantiate(rune.runePrefab, playerReference.transform.position, Quaternion.identity);
        }

        public void ResetMatching()
        {
            activeMatches.Clear();
            unactiveRunes = new List<Rune>(runes);
        }
    }
}