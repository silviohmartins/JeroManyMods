using BepInEx.Configuration;
using JeroManyMods.Patches.ContinuousLoadAmmo.Components;

namespace JeroManyMods.Config
{
    /// <summary>
    /// Classe base abstrata para todas as configurações de mods.
    /// Fornece acesso ao ConfigFile do BepInEx para criação de entradas de configuração.
    /// </summary>
    public abstract class ModConfig
    {
        protected ConfigFile Config { get; }

        protected ModConfig(ConfigFile config)
        {
            Config = config;
        }
    }

    /// <summary>
    /// Configurações do Easy Mode.
    /// Gerencia as configurações dos mods EnvironmentEnjoyer (remove árvores/arbustos) 
    /// e BushWhacker (remove arbustos e pântanos).
    /// </summary>
    public class EasyModeConfig : ModConfig
    {
        public ConfigEntry<bool> EnvironmentEnjoyer { get; private set; }
        public ConfigEntry<bool> BushWhacker { get; private set; }

        public EasyModeConfig(ConfigFile config) : base(config)
        {
            EnvironmentEnjoyer = Config.Bind(
                "1- Easy Mode",
                "EnvironmentEnjoyer - On/Off",
                false,
                new ConfigDescription("Enable EnvironmentEnjoyer - Disables trees and bushes",
                tags: new global::ConfigurationManagerAttributes() { Order = 2 }));

            BushWhacker = Config.Bind(
                "1- Easy Mode",
                "BushWhacker - On/Off",
                false,
                new ConfigDescription("Enable BushWhacker - Disables bushes and swamps",
                tags: new global::ConfigurationManagerAttributes() { Order = 1 }));
        }
    }

    /// <summary>
    /// Configurações do Skipper.
    /// Gerencia as configurações do mod que permite pular objetivos de quest.
    /// </summary>
    public class SkipperConfig : ModConfig
    {
        public ConfigEntry<bool> ModEnabled { get; private set; }
        public ConfigEntry<bool> AlwaysDisplay { get; private set; }
        public ConfigEntry<KeyboardShortcut> DisplayHotkey { get; private set; }

        public SkipperConfig(ConfigFile config) : base(config)
        {
            ModEnabled = Config.Bind(
                "2- Skipper",
                "Enabled",
                true,
                new ConfigDescription("Global mod toggle. Will need to re-open the quest window for the setting change to take effect.",
                tags: new global::ConfigurationManagerAttributes() { Order = 3 }));

            AlwaysDisplay = Config.Bind(
                "2- Skipper",
                "Always display Skip button",
                false,
                new ConfigDescription("If enabled, the Skip button will always be visible.",
                tags: new global::ConfigurationManagerAttributes() { Order = 2 }));

            DisplayHotkey = Config.Bind(
                "2- Skipper",
                "Display hotkey",
                new KeyboardShortcut(UnityEngine.KeyCode.LeftControl),
                new ConfigDescription("Holding down this key will make the Skip buttons appear.",
                tags: new global::ConfigurationManagerAttributes() { Order = 1 }));
        }
    }

    /// <summary>
    /// Configurações do Trader Scroller.
    /// Gerencia as configurações do mod que melhora a velocidade de scroll nos traders.
    /// </summary>
    public class TraderScrollerConfig : ModConfig
    {
        public ConfigEntry<float> ScrollWheelSpeed { get; private set; }

        public TraderScrollerConfig(ConfigFile config) : base(config)
        {
            ScrollWheelSpeed = Config.Bind(
                "3- Trader Scroller",
                "Scroll wheel speed",
                30f,
                new ConfigDescription("Adjusts scrolling speed when using mousewheel",
                tags: new global::ConfigurationManagerAttributes() { Order = 1 }));
        }
    }

    /// <summary>
    /// Configurações do VisorEffectManager.
    /// Gerencia as configurações do mod que remove efeitos visuais do visor (danos, arranhões, blur, distorção).
    /// Também gerencia as teclas de atalho para alternar essas configurações em tempo real.
    /// </summary>
    public class VisorEffectManagerConfig : ModConfig
    {
        public ConfigEntry<bool> RemoveGlassDamage { get; private set; }
        public ConfigEntry<bool> RemoveScratches { get; private set; }
        public ConfigEntry<bool> RemoveBlur { get; private set; }
        public ConfigEntry<bool> RemoveDistortion { get; private set; }

        public ConfigEntry<KeyboardShortcut> HotkeyGlassDamage { get; private set; }
        public ConfigEntry<KeyboardShortcut> HotkeyScratches { get; private set; }
        public ConfigEntry<KeyboardShortcut> HotkeyBlur { get; private set; }
        public ConfigEntry<KeyboardShortcut> HotkeyDistortion { get; private set; }

        public VisorEffectManagerConfig(ConfigFile config) : base(config)
        {
            RemoveGlassDamage = Config.Bind(
                "4- VisorEffectManager",
                "Remove Glass Damage",
                true,
                new ConfigDescription("Remove Glass Damage from visor",
                tags: new global::ConfigurationManagerAttributes() { Order = 4 }));

            RemoveScratches = Config.Bind(
                "4- VisorEffectManager",
                "Remove Scratches",
                true,
                new ConfigDescription("Remove Scratches from visor",
                tags: new global::ConfigurationManagerAttributes() { Order = 3 }));

            RemoveBlur = Config.Bind(
                "4- VisorEffectManager",
                "Remove Blur",
                true,
                new ConfigDescription("Remove Blur from visor",
                tags: new global::ConfigurationManagerAttributes() { Order = 2 }));

            RemoveDistortion = Config.Bind(
                "4- VisorEffectManager",
                "Remove Distortion",
                true,
                new ConfigDescription("Remove Distortion from visor",
                tags: new global::ConfigurationManagerAttributes() { Order = 1 }));

            HotkeyGlassDamage = Config.Bind(
                "4- VisorEffectManager",
                "Hotkey Glass Damage",
                new KeyboardShortcut(UnityEngine.KeyCode.Alpha1, UnityEngine.KeyCode.RightControl),
                new ConfigDescription("Hotkey to remove Glass Damage from visor",
                tags: new global::ConfigurationManagerAttributes() { Order = 4 }));

            HotkeyScratches = Config.Bind(
                "4- VisorEffectManager",
                "Hotkey Scratches",
                new KeyboardShortcut(UnityEngine.KeyCode.Alpha2, UnityEngine.KeyCode.RightControl),
                new ConfigDescription("Hotkey to remove Scratches from visor",
                tags: new global::ConfigurationManagerAttributes() { Order = 3 }));

            HotkeyBlur = Config.Bind(
                "4- VisorEffectManager",
                "Hotkey Blur",
                new KeyboardShortcut(UnityEngine.KeyCode.Alpha3, UnityEngine.KeyCode.RightControl),
                new ConfigDescription("Hotkey to remove Blur from visor",
                tags: new global::ConfigurationManagerAttributes() { Order = 2 }));

            HotkeyDistortion = Config.Bind(
                "4- VisorEffectManager",
                "Hotkey Distortion",
                new KeyboardShortcut(UnityEngine.KeyCode.Alpha4, UnityEngine.KeyCode.RightControl),
                new ConfigDescription("Hotkey to remove Distortion from visor",
                tags: new global::ConfigurationManagerAttributes() { Order = 1 }));
        }
    }

    /// <summary>
    /// Configurações do ContinuousLoadAmmo.
    /// Gerencia as configurações do mod que permite carregar munição continuamente fora do inventário.
    /// </summary>
    public class ContinuousLoadAmmoConfig : ModConfig
    {
        public ConfigEntry<float> SpeedLimit { get; private set; }
        public ConfigEntry<bool> ReachableOnly { get; private set; }
        public ConfigEntry<bool> InventoryTabs { get; private set; }
        public ConfigEntry<bool> PrioritizeHighestPenetration { get; private set; }
        public ConfigEntry<bool> QuickLoadNotify { get; private set; }
        public ConfigEntry<KeyboardShortcut> QuickLoadHotkey { get; private set; }

        public ContinuousLoadAmmoConfig(ConfigFile config) : base(config)
        {
            SpeedLimit = Config.Bind(
                "5- ContinuousLoadAmmo",
                "Speed Limit",
                0.31f,
                new ConfigDescription("The speed limit, as a percentage of the walk speed, set to the player while loading ammo",
                new AcceptableValueRange<float>(0f, 1f),
                tags: new global::ConfigurationManagerAttributes() { Order = 5, ShowRangeAsPercent = true }));

            ReachableOnly = Config.Bind(
                "5- ContinuousLoadAmmo",
                "Reachable Places Only",
                true,
                new ConfigDescription("Allow loading ammo outside the inventory only when Magazine and Ammo is in your Vest, Pockets, or Secure Container",
                null,
                tags: new global::ConfigurationManagerAttributes() { Order = 4 }));

            InventoryTabs = Config.Bind(
                "5- ContinuousLoadAmmo",
                "Inventory Tabs",
                true,
                new ConfigDescription("Do not interrupt loading ammo when switching inventory tabs (maps tab, tasks tab, etc.)",
                null,
                tags: new global::ConfigurationManagerAttributes() { Order = 3 }));

            QuickLoadHotkey = Config.Bind(
                "5- ContinuousLoadAmmo",
                "Quick Load Hotkey",
                new KeyboardShortcut(UnityEngine.KeyCode.K),
                new ConfigDescription("Key used to load ammo outside the inventory",
                null,
                tags: new global::ConfigurationManagerAttributes() { Order = 2 }));

            PrioritizeHighestPenetration = Config.Bind(
                "5- ContinuousLoadAmmo",
                "Prioritize Highest Penetration",
                true,
                new ConfigDescription("When using Quick Load, choose ammo that has the highest penetration power. If Disabled, prioritize the same ammo in the weapon's magazine",
                null,
                tags: new global::ConfigurationManagerAttributes() { Order = 1 }));

            QuickLoadNotify = Config.Bind(
                "5- ContinuousLoadAmmo",
                "Quick Load Notify",
                true,
                new ConfigDescription("When using Quick Load, notify the player of the ammo being loaded",
                null,
                tags: new global::ConfigurationManagerAttributes() { Order = 0 }));
        }
    }
    
    /// <summary>
    /// Configurações do ContinuousHealing.
    /// Gerencia as configurações do mod que permite curar partes do corpo continuamente.
    /// </summary>
    public class ContinuousHealingConfig : ModConfig
    {
        public ConfigEntry<bool> HealLimbs { get; private set; }
        public ConfigEntry<int> HealDelay { get; private set; }
        public ConfigEntry<bool> ResetAnimation { get; private set; }

        public ContinuousHealingConfig(ConfigFile config) : base(config)
        {
            HealLimbs = Config.Bind(
                "6- ContinuousHealing",
                "Heal Limbs",
                true,
                new ConfigDescription("If using surgery kits should also be continuous.\nNOTE: Animation does not loop.",
                null,
                tags: new global::ConfigurationManagerAttributes() { Order = 3 }));

            HealDelay = Config.Bind(
                "6- ContinuousHealing",
                "Heal Delay",
                10,
                new ConfigDescription("he delay between every heal on each limb. Game default is 2, set to 0 to use intended Continuous Healing behavior.",
                new AcceptableValueRange<int>(0, 5),
                tags: new global::ConfigurationManagerAttributes() { Order = 2 }));

            ResetAnimation = Config.Bind(
                "6- ContinuousHealing",
                "Reset Animation",
                true,
                new ConfigDescription("If a new animaton should be played between every limb being healed. This does not affect the speed at which you heal.",
                null,
                tags: new global::ConfigurationManagerAttributes() { Order = 1 }));
        }
    }

    /// <summary>
    /// Configurações do HideUI.
    /// Gerencia as configurações do mod que permite remover elementos UI do menu do jogo.
    /// </summary>
    public class HideUIConfig : ModConfig
    {
        public ConfigEntry<bool> HideBetaWarning { get; private set; }

        public HideUIConfig(ConfigFile config) : base(config)
        {
            HideBetaWarning = Config.Bind(
                "7- HideUI",
                "Hide Beta Warning",
                true,
                new ConfigDescription("Hide the Alpha/Beta warning message in the main menu",
                tags: new global::ConfigurationManagerAttributes() { Order = 1 }));
        }
    }
}

