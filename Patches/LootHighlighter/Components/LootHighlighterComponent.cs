using BepInEx.Configuration;
using BepInEx.Logging;
using Comfort.Common;
using EFT;
using JeroManyMods.Config;
using JeroManyMods.Managers;
using JeroManyMods.Patches.LootHighlighter.Managers;
using UnityEngine;

namespace JeroManyMods.Patches.LootHighlighter.Components
{
    /// <summary>
    /// Componente Unity que gerencia o highlight de loot em tempo de execução.
    /// Este componente é adicionado ao GameWorld quando uma raid inicia.
    /// </summary>
    public class LootHighlighterComponent : MonoBehaviour
    {
        private GameWorld _gameWorld;
        private Player _player;
        private float _lastCheckTime = 0f;
        private bool _isHighlightingEnabled = false;

        private LootDetectionManager _lootDetectionManager;
        private HighlightManager _highlightManager;
        private LootHighlighterConfig _config;

        private void Awake()
        {
            _gameWorld = Singleton<GameWorld>.Instance;

            if (_gameWorld == null)
            {
                MainJeroManyMods.Logger.LogError("[LootHighlighter] GameWorld not found.");
                Destroy(this);
                return;
            }

            _player = _gameWorld.MainPlayer;

            if (_player == null)
            {
                MainJeroManyMods.Logger.LogDebug("[LootHighlighter] No MainPlayer Found");
            }

            // Get config from main plugin
            _config = MainJeroManyMods.ConfigInstance;

            // Initialize managers
            _lootDetectionManager = new LootDetectionManager(MainJeroManyMods.Logger);
            _highlightManager = new HighlightManager(MainJeroManyMods.Logger, _config);
            _highlightManager.Initialize();

            // Start with highlighting enabled if configured
            _isHighlightingEnabled = _config.EnabledPlugin.Value;
        }

        private void Update()
        {
            if (!_config.EnabledPlugin.Value)
                return;

            // Toggle highlighting with keyboard shortcut
            if (IsKeyPressed(_config.ToggleKeyboardShortcut.Value))
            {
                ToggleHighlighting();
            }

            if (!_isHighlightingEnabled)
                return;

            // Check for loot periodically
            if (Time.time - _lastCheckTime > _config.CheckInterval.Value)
            {
                _lastCheckTime = Time.time;
                DetectAndHighlightLoot();
                _highlightManager.CleanupStaleHighlights();
            }
        }

        private void DetectAndHighlightLoot()
        {
            if (Camera.main == null || _player == null)
                return;

            Vector3 playerPos = Camera.main.transform.position;
            float detectionRadius = _config.DetectionRadius.Value;

            var detectedLoot = _lootDetectionManager.DetectNearbyLoot(playerPos, detectionRadius, _config);

            foreach (var lootInfo in detectedLoot)
            {
                _highlightManager.CreateOrUpdateHighlight(lootInfo, playerPos);
            }
        }

        private void ToggleHighlighting()
        {
            _isHighlightingEnabled = !_isHighlightingEnabled;
            MainJeroManyMods.Logger.LogInfo($"Loot Highlighting {(_isHighlightingEnabled ? "enabled" : "disabled")}");

            if (!_isHighlightingEnabled)
            {
                _highlightManager.CleanupAllHighlights();
            }
        }

        bool IsKeyPressed(KeyboardShortcut key)
        {
            return key.IsDown();
        }

        private void OnDestroy()
        {
            MainJeroManyMods.Logger?.LogInfo("[LootHighlighter] LootHighlighterComponent is being destroyed");

            // Cleanup all highlights
            if (_isHighlightingEnabled)
            {
                _highlightManager?.CleanupAllHighlights();
            }

            // Cleanup managers
            _lootDetectionManager?.Cleanup();
            _highlightManager?.Cleanup();

            // Clear references to prevent memory leaks
            _gameWorld = null;
            _player = null;
            _lootDetectionManager = null;
            _highlightManager = null;
        }
    }
}

