using JeroManyMods.Config;
using UnityEngine;

namespace JeroManyMods.Patches
{
    /// <summary>
    /// Componente que monitora a hotkey do Skipper e atualiza a visibilidade dos botões.
    /// Usa MonoBehaviour para garantir que seja atualizado mesmo quando menus estão abertos.
    /// </summary>
    public class SkipperHotkeyComponent : MonoBehaviour
    {
        private SkipperConfig _config;
        private bool _lastHotkeyState;

        public void Initialize(SkipperConfig config)
        {
            _config = config;
            _lastHotkeyState = false;
        }

        private void Update()
        {
            if (_config == null || !_config.ModEnabled.Value || _config.AlwaysDisplay.Value)
            {
                return;
            }

            if (QuestObjectiveViewPatch.LastSeenObjectivesBlock == null || 
                !QuestObjectiveViewPatch.LastSeenObjectivesBlock.activeSelf ||
                QuestObjectiveViewPatch.LastSeenObjectivesBlock != gameObject)
            {
                return;
            }

            // Verifica se a hotkey está sendo pressionada usando Input.GetKey do Unity
            bool isHotkeyPressed = Input.GetKey(_config.DisplayHotkey.Value.MainKey);
            
            // Verifica se há mods pressionados (Ctrl, Alt, Shift)
            if (_config.DisplayHotkey.Value.Modifiers != null)
            {
                foreach (var modifier in _config.DisplayHotkey.Value.Modifiers)
                {
                    if (!Input.GetKey(modifier))
                    {
                        isHotkeyPressed = false;
                        break;
                    }
                }
            }

            // Só atualiza se o estado mudou para evitar chamadas desnecessárias
            if (isHotkeyPressed != _lastHotkeyState)
            {
                _lastHotkeyState = isHotkeyPressed;
                ChangeButtonVisibility(isHotkeyPressed);
            }
        }

        private void ChangeButtonVisibility(bool setVisibilityTo)
        {
            if (QuestObjectiveViewPatch.LastSeenObjectivesBlock == null)
            {
                return;
            }

            foreach (var button in QuestObjectiveViewPatch.LastSeenObjectivesBlock.GetComponentsInChildren<EFT.UI.DefaultUIButton>(includeInactive: true))
            {
                if (button == null || button.name != MainJeroManyMods.SkipButtonName) continue;

                button.gameObject.SetActive(setVisibilityTo);
            }
        }
    }
}

