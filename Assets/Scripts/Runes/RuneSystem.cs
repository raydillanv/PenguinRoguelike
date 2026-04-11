using System;
using System.Collections.Generic;
using System.Linq;
using Spells;
using UnityEngine;

namespace Runes
{
    public class RuneSystem : MonoBehaviour
    {
        public SpellCaster spellCasterReference;

        public List<Rune> runes = new List<Rune>();
        private Dictionary<Rune, int> activeMatches = new Dictionary<Rune, int>();
        private Dictionary<Rune, float> cooldowns = new Dictionary<Rune, float>();
        private List<Rune> unactiveRunes = new List<Rune>();

        public IReadOnlyDictionary<Rune, int> ActiveMatches => activeMatches;
        public IReadOnlyDictionary<Rune, float> Cooldowns => cooldowns;
        public float matchThreshold = 0.85f;
        public bool debug;

        public void Start()
        {
            activeMatches = new Dictionary<Rune, int>();
            cooldowns = new Dictionary<Rune, float>();
            unactiveRunes = new List<Rune>(runes);

            RefreshReferences();
        }

        public float GetCooldownProgress(Rune rune)
        {
            if (!cooldowns.TryGetValue(rune, out float castTime)) return 1f;
            float elapsed = Time.time - castTime;
            if (elapsed >= rune.cooldown)
            {
                cooldowns.Remove(rune);
                return 1f;
            }
            return elapsed / rune.cooldown;
        }

        public bool IsOnCooldown(Rune rune)
        {
            if (!cooldowns.TryGetValue(rune, out float castTime)) return false;
            if (Time.time - castTime >= rune.cooldown)
            {
                cooldowns.Remove(rune);
                return false;
            }
            return true;
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
                activeMatches.Remove(rune);
                unactiveRunes.Add(rune);
                OnRuneComplete(rune);
            }

            // Handle new direction matches (skip runes on cooldown)
            var newRunes = new List<Rune>();
            foreach (var rune in unactiveRunes.Where(rune => !IsOnCooldown(rune) && Vector2.Dot(direction, rune.Vectors[0]) >= matchThreshold))
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
            cooldowns[rune] = Time.time;
            
            if (rune.runePrefab && GameManager.instance.player)
                rune.runePrefab.GetComponent<Spell>().Cast(spellCasterReference); 
        }

        public void RefreshReferences()
        {
            if (spellCasterReference) return;
            GameManager.instance.RefreshPlayerReference();
            spellCasterReference = GameManager.instance.player.GetComponent<SpellCaster>();
        }

        public void ResetMatching()
        {
            activeMatches.Clear();
            unactiveRunes = new List<Rune>(runes);
        }
    }
}