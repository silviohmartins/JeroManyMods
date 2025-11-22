using JetBrains.Annotations;
using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;
using UnityEngine;
using Comfort.Common;
using EFT;
using JeroManyMods.Config;
using JeroManyMods.Managers;
using JeroManyMods.Patches;
using JeroManyMods.Patches.ContinuousLoadAmmo.Patches;
using ScreensPatches = JeroManyMods.Patches.ContinuousLoadAmmo.Patches.ScreensPatches;
using JeroManyMods.Patches.TraderScrolling;
using JeroManyMods.Patches.VisorEffectManager;
using JeroManyMods.Patches.ContinuousHealing;
using JeroManyMods.Patches.HideUI.Patches;
using LootHighlighterPatches = JeroManyMods.Patches.LootHighlighter.Patches;
using HealingAutoCancelPatches = JeroManyMods.Patches.HealingAutoCancel.Patches;

namespace JeroManyMods
{
    [BepInPlugin("com.jero.manymods", "JeroManyMods", "1.0.0")]
    [BepInDependency("Tyfon.UIFixes", BepInDependency.DependencyFlags.SoftDependency)]
    public class MainJeroManyMods : BaseUnityPlugin
    {
        internal new static ManualLogSource Logger { get; private set; }
        internal new static ConfigFile Config { get; private set; }
        internal static LootHighlighterConfig ConfigInstance { get; private set; }

        // Configurações
        private EasyModeConfig _easyModeConfig;
        private SkipperConfig _skipperConfig;
        private TraderScrollerConfig _traderScrollerConfig;
        private VisorEffectManagerConfig _visorEffectManagerConfig;
        private ContinuousLoadAmmoConfig _continuousLoadAmmoConfig;
        private ContinuousHealingConfig _continuousHealingConfig;
        private HideUIConfig _hideUIConfig;
        private LootHighlighterConfig _lootHighlighterConfig;
        private HealingAutoCancelConfig _healingAutoCancelConfig;

        // Managers
        private PatchManager _patchManager;
        private InputManager _inputManager;
        private SkipperManager _skipperManager;


        /// <summary>
        /// Método chamado quando o plugin é carregado pelo BepInEx.
        /// Inicializa todas as configurações, managers e patches.
        /// </summary>
        public void Awake()
        {
            Logger = base.Logger;
            Config = base.Config;
            
            // Inicializar configurações
            InitializeConfigurations();

            // Inicializar managers
            InitializeManagers();

            // Habilitar patches
            EnablePatches();
        }

        private void InitializeConfigurations()
        {
            _easyModeConfig = new EasyModeConfig(Config);
            _skipperConfig = new SkipperConfig(Config);
            _traderScrollerConfig = new TraderScrollerConfig(Config);
            _visorEffectManagerConfig = new VisorEffectManagerConfig(Config);
            _continuousLoadAmmoConfig = new ContinuousLoadAmmoConfig(Config);
            _continuousHealingConfig = new ContinuousHealingConfig(Config);
            _hideUIConfig = new HideUIConfig(Config);
            _lootHighlighterConfig = new LootHighlighterConfig(Config);
            _healingAutoCancelConfig = new HealingAutoCancelConfig(Config);
            ConfigInstance = _lootHighlighterConfig;

            // Manter compatibilidade com código existente (propriedades estáticas)
            EnvironmentEnjoyer = _easyModeConfig.EnvironmentEnjoyer;
            BushWhacker = _easyModeConfig.BushWhacker;
            ModEnabled = _skipperConfig.ModEnabled;
            AlwaysDisplay = _skipperConfig.AlwaysDisplay;
            DisplayHotkey = _skipperConfig.DisplayHotkey;
            ScrollWheelSpeed = _traderScrollerConfig.ScrollWheelSpeed;
            RemoveGlassDamage = _visorEffectManagerConfig.RemoveGlassDamage;
            RemoveScratches = _visorEffectManagerConfig.RemoveScratches;
            RemoveBlur = _visorEffectManagerConfig.RemoveBlur;
            RemoveDistortion = _visorEffectManagerConfig.RemoveDistortion;
            HotkeyGlassDamage = _visorEffectManagerConfig.HotkeyGlassDamage;
            HotkeyScratches = _visorEffectManagerConfig.HotkeyScratches;
            HotkeyBlur = _visorEffectManagerConfig.HotkeyBlur;
            HotkeyDistortion = _visorEffectManagerConfig.HotkeyDistortion;
            
            // ContinuousLoadAmmo
            SpeedLimit = _continuousLoadAmmoConfig.SpeedLimit;
            ReachableOnly = _continuousLoadAmmoConfig.ReachableOnly;
            InventoryTabs = _continuousLoadAmmoConfig.InventoryTabs;
            QuickLoadHotkey = _continuousLoadAmmoConfig.QuickLoadHotkey;
            PrioritizeHighestPenetration = _continuousLoadAmmoConfig.PrioritizeHighestPenetration;
            QuickLoadNotify = _continuousLoadAmmoConfig.QuickLoadNotify;

            // ContinuousHealing
            HealLimbs = _continuousHealingConfig.HealLimbs;
            HealDelay = _continuousHealingConfig.HealDelay;
            ResetAnimation = _continuousHealingConfig.ResetAnimation;

            // HideUI
            HideBetaWarning = _hideUIConfig.HideBetaWarning;
        }

        /// <summary>
        /// Inicializa todos os managers do mod.
        /// </summary>
        private void InitializeManagers()
        {
            _patchManager = new PatchManager(Logger);
            _inputManager = new InputManager(Logger, _visorEffectManagerConfig);
            _skipperManager = new SkipperManager(Logger, _skipperConfig);

            // Inicializar managers
            _patchManager.Initialize();
            _inputManager.Initialize();
            _skipperManager.Initialize();
        }

        /// <summary>
        /// Habilita todos os patches do mod usando o PatchManager.
        /// </summary>
        private void EnablePatches()
        {
            // Inicializar HealingAutoCancel patch com configuração
            HealingAutoCancelPatches.GameWorldPatch.Initialize(_healingAutoCancelConfig);

            _patchManager.EnablePatches(
                (new ManyModsPatch(), "ManyModsPatch (Easy Mode)"),
                (new QuestObjectiveViewPatch(), "QuestObjectiveViewPatch (Skipper)"),
                (new TraderScrollingPatch(), "TraderScrollingPatch"),
                (new PlayerCardPatch(), "PlayerCardPatch"),
                (new FaceShieldPatch(), "FaceShieldPatch (VisorEffectManager)"),
                (new LoadMagazineStartPatch(), "LoadMagazineStartPatch (ContinuousLoadAmmo)"),
                (new UnloadMagazineStartPatch(), "UnloadMagazineStartPatch (ContinuousLoadAmmo)"),
                (new InventoryScreenClosePatch(), "InventoryScreenClosePatch (ContinuousLoadAmmo)"),
                (new RegisterPlayerPatch(), "RegisterPlayerPatch (ContinuousLoadAmmo)"),
                (new CH_StartHeal_Patch(), "CH_StartHeal_Patch (ContinuousHealing)"),
                (new CH_EndHeal_Patch(), "CH_EndHeal_Patch (ContinuousHealing)"),
                (new CH_CancelHeal_Patch(), "CH_CancelHeal_Patch (ContinuousHealing)"),
                (new HideBetaPatch(), "HideBetaPatch (HideUI)"),
                (new LootHighlighterPatches.GameWorldPatch(), "GameWorldPatch (LootHighlighter)"),
                (new HealingAutoCancelPatches.GameWorldPatch(), "GameWorldPatch (HealingAutoCancel)")
            );

            // Habilitar ScreensPatches (método estático)
            ScreensPatches.Enable();
        }

        /// <summary>
        /// Método chamado a cada frame pelo Unity.
        /// Processa inputs e atualiza todos os managers que precisam de atualização por frame.
        /// </summary>
        [UsedImplicitly]
        internal void Update()
        {
            // Processar inputs do VisorEffectManager (prioridade alta)
            _inputManager?.Update();

            // Processar inputs do Skipper
            _skipperManager?.Update();
        }

        // Propriedades estáticas para compatibilidade com código existente
        internal const string SkipButtonName = SkipperManager.SkipButtonName;
        internal static ConfigEntry<bool> EnvironmentEnjoyer { get; private set; }
        internal static ConfigEntry<bool> BushWhacker { get; private set; }
        internal static ConfigEntry<bool> ModEnabled { get; private set; }
        internal static ConfigEntry<bool> AlwaysDisplay { get; private set; }
        internal static ConfigEntry<KeyboardShortcut> DisplayHotkey { get; private set; }
        internal static ConfigEntry<float> ScrollWheelSpeed { get; private set; }
        internal static ConfigEntry<bool> RemoveGlassDamage { get; private set; }
        internal static ConfigEntry<bool> RemoveScratches { get; private set; }
        internal static ConfigEntry<bool> RemoveBlur { get; private set; }
        internal static ConfigEntry<bool> RemoveDistortion { get; private set; }
        internal static ConfigEntry<KeyboardShortcut> HotkeyGlassDamage { get; private set; }
        internal static ConfigEntry<KeyboardShortcut> HotkeyScratches { get; private set; }
        internal static ConfigEntry<KeyboardShortcut> HotkeyBlur { get; private set; }
        internal static ConfigEntry<KeyboardShortcut> HotkeyDistortion { get; private set; }

        // ContinuousLoadAmmo - Propriedades estáticas para compatibilidade
        internal static ConfigEntry<float> SpeedLimit { get; private set; }
        internal static ConfigEntry<bool> ReachableOnly { get; private set; }
        internal static ConfigEntry<bool> InventoryTabs { get; private set; }
        internal static ConfigEntry<KeyboardShortcut> QuickLoadHotkey { get; private set; }
        internal static ConfigEntry<bool> PrioritizeHighestPenetration { get; private set; }
        internal static ConfigEntry<bool> QuickLoadNotify { get; private set; }

        /// <summary>
        /// Verifica se o jogador está em uma raid.
        /// </summary>
        public static bool InRaid => Singleton<AbstractGame>.Instance is AbstractGame abstractGame && abstractGame.InRaid;

        // ContinuousHealing - Propriedades estáticas para compatibilidade
        public static ConfigEntry<bool> HealLimbs { get; set; }
        public static ConfigEntry<int> HealDelay { get; set; }
        public static ConfigEntry<bool> ResetAnimation { get; set; }

        // HideUI - Propriedades estáticas para compatibilidade
        internal static ConfigEntry<bool> HideBetaWarning { get; private set; }

    }
}
