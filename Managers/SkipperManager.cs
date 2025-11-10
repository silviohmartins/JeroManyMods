using BepInEx.Logging;
using EFT.UI;
using JeroManyMods.Config;
using JeroManyMods.Patches;
using UnityEngine;

namespace JeroManyMods.Managers
{
    /// <summary>
    /// Gerencia a funcionalidade do Skipper (pular objetivos de quest).
    /// Controla a visibilidade dos botões de skip e processa inputs do teclado.
    /// </summary>
    public class SkipperManager : BaseManager
    {
        private readonly SkipperConfig _config;
        
        /// <summary>
        /// Nome do botão de skip usado para identificação
        /// </summary>
        public const string SkipButtonName = "SkipButton";

        /// <summary>
        /// Inicializa uma nova instância do SkipperManager
        /// </summary>
        /// <param name="logger">Logger para mensagens de log</param>
        /// <param name="config">Configurações do Skipper</param>
        public SkipperManager(ManualLogSource logger, SkipperConfig config) : base(logger)
        {
            _config = config ?? throw new System.ArgumentNullException(nameof(config));
        }

        /// <summary>
        /// Processa inputs e lógica do Skipper a cada frame.
        /// Verifica se o hotkey foi pressionado e atualiza a visibilidade dos botões.
        /// </summary>
        public override void Update()
        {
            if (!_config.ModEnabled.Value || _config.AlwaysDisplay.Value)
            {
                return;
            }

            if (QuestObjectiveViewPatch.LastSeenObjectivesBlock == null || !QuestObjectiveViewPatch.LastSeenObjectivesBlock.activeSelf)
            {
                return;
            }

            if (_config.DisplayHotkey.Value.IsDown())
            {
                ChangeButtonVisibility(true);
            }

            if (_config.DisplayHotkey.Value.IsUp())
            {
                ChangeButtonVisibility(false);
            }
        }

        /// <summary>
        /// Altera a visibilidade de todos os botões de skip na interface
        /// </summary>
        /// <param name="setVisibilityTo">True para mostrar, False para esconder</param>
        private void ChangeButtonVisibility(bool setVisibilityTo)
        {
            foreach (var button in QuestObjectiveViewPatch.LastSeenObjectivesBlock.GetComponentsInChildren<DefaultUIButton>(includeInactive: true))
            {
                if (button.name != SkipButtonName) continue;

                button.gameObject.SetActive(setVisibilityTo);
            }
        }
    }
}

