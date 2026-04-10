using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Runes
{
    public class RuneToolbar : MonoBehaviour
    {
        public RuneSystem runeSystem;
        public int textureSize = 64;
        public int spacing = 10;
        public int maxPerRow = 6;
        public int lineThickness = 3;
        public Color baseColor = Color.gray;
        public Color matchedColor = Color.cyan;
        public Color completeColor = Color.green;
        public Color cooldownColor = new Color(0.5f, 0.2f, 0.2f);

        private Dictionary<Rune, RawImage> runeImages = new Dictionary<Rune, RawImage>();
        private Dictionary<Rune, Texture2D> runeTextures = new Dictionary<Rune, Texture2D>();

        private void Start()
        {
            if (!runeSystem) return;

            var grid = gameObject.AddComponent<GridLayoutGroup>();
            grid.cellSize = new Vector2(textureSize, textureSize);
            grid.spacing = new Vector2(spacing, spacing);
            grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            grid.constraintCount = maxPerRow;

            foreach (var rune in runeSystem.runes)
            {
                var go = new GameObject(rune.name);
                go.transform.SetParent(transform, false);

                var img = go.AddComponent<RawImage>();
                var tex = new Texture2D(textureSize, textureSize);
                tex.filterMode = FilterMode.Point;
                img.texture = tex;

                runeImages[rune] = img;
                runeTextures[rune] = tex;
            }
        }

        private void Update()
        {
            if (!runeSystem) return;

            foreach (var rune in runeSystem.runes)
            {
                int matched = runeSystem.ActiveMatches.TryGetValue(rune, out int m) ? m : 0;
                DrawRune(rune, matched);
            }
        }

        private void DrawRune(Rune rune, int matchedCount)
        {
            if (!runeTextures.TryGetValue(rune, out var tex)) return;

            // Clear
            Color[] pixels = new Color[textureSize * textureSize];
            for (int i = 0; i < pixels.Length; i++) pixels[i] = Color.clear;
            tex.SetPixels(pixels);

            if (rune.Size == 0) { tex.Apply(); return; }

            // Calculate bounds of shape
            var points = new List<Vector2> { Vector2.zero };
            Vector2 pos = Vector2.zero;
            foreach (var dir in rune.Vectors)
            {
                pos += dir.normalized;
                points.Add(pos);
            }

            Vector2 min = points[0], max = points[0];
            foreach (var p in points)
            {
                min = Vector2.Min(min, p);
                max = Vector2.Max(max, p);
            }

            // Scale to fit with padding
            float padding = 4f;
            Vector2 size = max - min;
            float scale = (textureSize - padding * 2) / Mathf.Max(size.x, size.y, 0.01f);
            Vector2 offset = new Vector2(textureSize / 2f, textureSize / 2f) - (min + max) / 2f * scale;

            // Check cooldown state from RuneSystem
            bool onCooldown = runeSystem.IsOnCooldown(rune);
            float cooldownProgress = runeSystem.GetCooldownProgress(rune);

            // Draw
            pos = Vector2.zero * scale + offset;
            for (int i = 0; i < rune.Size; i++)
            {
                Vector2 next = pos + rune.Vectors[i].normalized * scale;

                Color lineColor;
                if (onCooldown)
                {
                    // Brief green flash at start, then transition from red to gray
                    if (cooldownProgress < 0.25f)
                    {
                        lineColor = completeColor;
                    } else {
                        float segmentProgress = (rune.Size - i) / (float)rune.Size;
                        lineColor = segmentProgress <= cooldownProgress ? baseColor : cooldownColor;
                    }
                } else {
                    lineColor = i < matchedCount ? matchedColor : baseColor;
                }

                DrawLine(tex, pos, next, lineColor);
                pos = next;
            }

            tex.Apply();
        }

        private void DrawLine(Texture2D tex, Vector2 from, Vector2 to, Color color)
        {
            int steps = Mathf.CeilToInt((to - from).magnitude * 2);
            for (int i = 0; i <= steps; i++)
            {
                Vector2 p = Vector2.Lerp(from, to, i / (float)steps);
                int cx = Mathf.RoundToInt(p.x);
                int cy = Mathf.RoundToInt(p.y);

                for (int dx = -lineThickness / 2; dx <= lineThickness / 2; dx++)
                {
                    for (int dy = -lineThickness / 2; dy <= lineThickness / 2; dy++)
                    {
                        int x = Mathf.Clamp(cx + dx, 0, tex.width - 1);
                        int y = Mathf.Clamp(cy + dy, 0, tex.height - 1);
                        tex.SetPixel(x, y, color);
                    }
                }
            }
        }
    }
}
