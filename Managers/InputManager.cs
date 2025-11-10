using System;
using BepInEx.Configuration;
using BepInEx.Logging;
using JeroManyMods.Config;

namespace JeroManyMods.Managers
{
    /// <summary>
    /// Gerencia inputs de teclado para os mods que requerem interação via teclas de atalho.
    /// Atualmente gerencia os inputs do VisorEffectManager.
    /// </summary>
    public class InputManager : BaseManager
    {
        private readonly VisorEffectManagerConfig _visorConfig;

        /// <summary>
        /// Inicializa uma nova instância do InputManager
        /// </summary>
        /// <param name="logger">Logger para mensagens de log</param>
        /// <param name="visorConfig">Configurações do VisorEffectManager</param>
        public InputManager(ManualLogSource logger, VisorEffectManagerConfig visorConfig) : base(logger)
        {
            _visorConfig = visorConfig ?? throw new ArgumentNullException(nameof(visorConfig));
        }

        /// <summary>
        /// Processa inputs do VisorEffectManager.
        /// Verifica se as teclas de atalho foram pressionadas e alterna as configurações correspondentes.
        /// </summary>
        public override void Update()
        {
            if (_visorConfig.HotkeyGlassDamage?.Value != null && _visorConfig.HotkeyGlassDamage.Value.IsDown())
            {
                ToggleSetting(_visorConfig.RemoveGlassDamage, "Glass Damage");
            }
            else if (_visorConfig.HotkeyScratches?.Value != null && _visorConfig.HotkeyScratches.Value.IsDown())
            {
                ToggleSetting(_visorConfig.RemoveScratches, "Scratches");
            }
            else if (_visorConfig.HotkeyBlur?.Value != null && _visorConfig.HotkeyBlur.Value.IsDown())
            {
                ToggleSetting(_visorConfig.RemoveBlur, "Blur");
            }
            else if (_visorConfig.HotkeyDistortion?.Value != null && _visorConfig.HotkeyDistortion.Value.IsDown())
            {
                ToggleSetting(_visorConfig.RemoveDistortion, "Distortion");
            }
        }

        private void ToggleSetting(ConfigEntry<bool> setting, string settingName)
        {
            if (setting == null)
            {
                return;
            }

            try
            {
                bool newValue = !setting.Value;
                setting.Value = newValue;

                Logger.LogInfo($"VisorEffectManager: {settingName} {(newValue ? "enabled" : "disabled")} via keyboard shortcut");
                Logger.LogInfo($"VisorEffectManager: Change will be applied when visor is updated (equip/unequip or new raid)");
            }
            catch (Exception ex)
            {
                Logger.LogError($"VisorEffectManager: Error toggling {settingName} - {ex.Message}");
            }
        }
    }
}

