using System.Collections.Generic;
using BepInEx.Logging;
using EFT.Interactive;
using JeroManyMods.Config;
using JeroManyMods.Managers;
using JeroManyMods.Patches.LootHighlighter.Components;
using UnityEngine;

namespace JeroManyMods.Patches.LootHighlighter.Managers
{
    /// <summary>
    /// Gerencia a criação e atualização dos highlights visuais de loot.
    /// Cria luzes e labels de texto para destacar objetos de loot.
    /// </summary>
    public class HighlightManager : BaseManager
    {
        private class HighlightInfo
        {
            public GameObject LabelObject;
            public Light HighlightLight;
            public float LastSeenTime;
            public string Category;

            public HighlightInfo(GameObject label, Light light, string category)
            {
                LabelObject = label;
                HighlightLight = light;
                LastSeenTime = Time.time;
                Category = category;
            }
        }

        private Dictionary<int, HighlightInfo> _highlightedObjects;
        private Dictionary<string, int> _categoryStats;
        private LootHighlighterConfig _config;

        public HighlightManager(ManualLogSource logger, LootHighlighterConfig config) : base(logger)
        {
            _config = config;
            _highlightedObjects = new Dictionary<int, HighlightInfo>();
            _categoryStats = new Dictionary<string, int>()
            {
                { "Items", 0 },
                { "Containers", 0 },
                { "Corpses", 0 }
            };
        }

        public override void Initialize()
        {
            // Initialize if needed
        }

        /// <summary>
        /// Cria ou atualiza um highlight para um item de loot
        /// </summary>
        public void CreateOrUpdateHighlight(LootDetectionManager.LootInfo lootInfo, Vector3 playerPos)
        {
            int objectId = lootInfo.ObjectId;

            // Update last seen time if already tracked
            if (_highlightedObjects.ContainsKey(objectId))
            {
                _highlightedObjects[objectId].LastSeenTime = Time.time;
                return;
            }

            // Create new highlight
            CreateHighlight(lootInfo.GameObject, lootInfo.DisplayName, objectId, lootInfo.Category);
        }

        private void CreateHighlight(GameObject target, string labelText, int objectId, string category)
        {
            if (target == null)
                return;

            // Get color based on category
            Color labelColor = GetCategoryColor(category);

            // Override with rarity color for items
            if (category == "Items" && target.TryGetComponent<LootItem>(out var loot))
            {
                string templateId = loot.Item?.TemplateId ?? "";
                labelColor = GetRarityColor(templateId);
            }

            // Create label object (optional based on config)
            GameObject labelObj = null;
            if (_config.ShowTextLabels.Value)
            {
                labelObj = new GameObject("LootLabel");
                labelObj.transform.SetParent(target.transform);
                labelObj.transform.localPosition = new Vector3(0, 0.2f, 0);

                // Add text component
                TextMesh textMesh = labelObj.AddComponent<TextMesh>();
                textMesh.text = labelText;
                textMesh.characterSize = 0.03f;
                textMesh.alignment = TextAlignment.Center;
                textMesh.anchor = TextAnchor.MiddleCenter;
                textMesh.color = labelColor;

                // Make text face camera
                labelObj.AddComponent<Billboard>();
            }

            // Add highlight light (always shown)
            Light light = target.AddComponent<Light>();
            light.range = 2f;
            light.intensity = 1.5f;
            light.type = LightType.Point;
            light.color = labelColor;

            // Track this highlight for later cleanup
            _highlightedObjects[objectId] = new HighlightInfo(labelObj, light, category);
        }

        private Color GetCategoryColor(string category)
        {
            switch (category)
            {
                case "Items":
                    return _config.ItemsColor.Value;
                case "Containers":
                    return _config.ContainersColor.Value;
                case "Corpses":
                    return _config.CorpsesColor.Value;
                default:
                    return Color.white;
            }
        }

        private Color GetRarityColor(string templateId)
        {
            // Enhanced rarity system with more tiers
            if (string.IsNullOrEmpty(templateId))
                return Color.white;

            // Legendary/Ultra Rare (specific high-value items)
            if (templateId.StartsWith("5c0") || templateId.StartsWith("5fc"))
                return new Color(1f, 0f, 1f); // Purple for ultra rare

            // Rare items
            if (templateId.StartsWith("5c") || templateId.StartsWith("5a"))
                return Color.yellow;

            // Uncommon items
            if (templateId.StartsWith("59") || templateId.StartsWith("56"))
                return Color.cyan;

            // Common items
            if (templateId.StartsWith("54") || templateId.StartsWith("57"))
                return Color.white;

            // Default for unknown template IDs
            return Color.white;
        }

        public void CleanupStaleHighlights()
        {
            List<int> objectsToRemove = new List<int>();
            float staleThreshold = 2.0f; // Remove highlights after 2 seconds of not seeing them

            foreach (var kvp in _highlightedObjects)
            {
                // Remove immediately marked objects or those not seen for a while
                if (kvp.Value.LastSeenTime < 0 || (Time.time - kvp.Value.LastSeenTime > staleThreshold))
                {
                    objectsToRemove.Add(kvp.Key);
                }
            }

            // Clean up all flagged objects
            foreach (int objectId in objectsToRemove)
            {
                CleanupHighlight(objectId);
            }
        }

        public void CleanupHighlight(int objectId)
        {
            if (!_highlightedObjects.ContainsKey(objectId))
                return;

            var info = _highlightedObjects[objectId];

            // Clean up label
            if (info.LabelObject != null)
            {
                Object.Destroy(info.LabelObject);
            }

            // Clean up light
            if (info.HighlightLight != null)
            {
                Object.Destroy(info.HighlightLight);
            }

            // Remove from tracking
            _highlightedObjects.Remove(objectId);
        }

        public void CleanupAllHighlights()
        {
            foreach (var objectId in new List<int>(_highlightedObjects.Keys))
            {
                CleanupHighlight(objectId);
            }
            _highlightedObjects.Clear();
        }

        public int GetTotalHighlightCount()
        {
            return _highlightedObjects.Count;
        }

        public Dictionary<string, int> GetCategoryStats()
        {
            // Reset counts
            foreach (var key in new List<string>(_categoryStats.Keys))
            {
                _categoryStats[key] = 0;
            }

            // Count by category
            foreach (var highlight in _highlightedObjects.Values)
            {
                if (_categoryStats.ContainsKey(highlight.Category))
                {
                    _categoryStats[highlight.Category]++;
                }
            }

            return _categoryStats;
        }

        public override void Cleanup()
        {
            CleanupAllHighlights();
        }
    }
}

