using System.Reflection;
using SPT.Reflection.Patching;
using Comfort.Common;
using EFT;
using EFT.HealthSystem;
using HarmonyLib;
using JeroManyMods.Config;

namespace JeroManyMods.Patches.HealingAutoCancel.Patches
{
    /// <summary>
    /// Patch que implementa o cancelamento automático de cura quando a parte do corpo está totalmente curada
    /// e não está sangrando ou quebrada.
    /// Baseado no mod HealingAutoCancel de minihazel.
    /// </summary>
    public class GameWorldPatch : ModulePatch
    {
        private static Player _player;
        private static ActiveHealthController _activeHealthController;
        private static HealingAutoCancelConfig _config;

        /// <summary>
        /// Inicializa o patch com a configuração.
        /// </summary>
        /// <param name="config">Configuração do HealingAutoCancel</param>
        public static void Initialize(HealingAutoCancelConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// Obtém o método alvo do patch: GameWorld.OnGameStarted
        /// </summary>
        /// <returns>MethodInfo do método OnGameStarted</returns>
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(GameWorld), nameof(GameWorld.OnGameStarted));
        }

        /// <summary>
        /// Método executado após OnGameStarted ser chamado.
        /// Registra o evento HealthChangedEvent para monitorar mudanças de saúde e cancelar cura automaticamente.
        /// </summary>
        [PatchPostfix]
        public static void PatchPostFix()
        {
            if (_config == null || !_config.EnableAutoHealCanceling.Value)
            {
                return;
            }

            var gameWorld = Singleton<GameWorld>.Instance;

            if (gameWorld == null || gameWorld.MainPlayer == null)
            {
                return;
            }

            _player = gameWorld.MainPlayer;
            _activeHealthController = _player.ActiveHealthController;

            if (_activeHealthController != null)
            {
                _activeHealthController.HealthChangedEvent += ActiveHealthController_HealthChangedEvent;
                MainJeroManyMods.Logger.LogInfo("HealingAutoCancel: Evento HealthChangedEvent registrado com sucesso");
            }
        }

        /// <summary>
        /// Manipulador do evento HealthChangedEvent.
        /// Cancela automaticamente a cura quando a parte do corpo está totalmente curada e não está sangrando ou quebrada.
        /// </summary>
        /// <param name="bodyPart">Parte do corpo que teve mudança de saúde</param>
        /// <param name="amount">Quantidade de mudança</param>
        /// <param name="damageInfo">Informações sobre o dano/cura</param>
        private static void ActiveHealthController_HealthChangedEvent(EBodyPart bodyPart, float amount, DamageInfoStruct damageInfo)
        {
            // Só processa mudanças causadas por medicina
            if (damageInfo.DamageType != EDamageType.Medicine)
            {
                return;
            }

            // Verifica se há um medkit nas mãos do jogador
            MedsItemClass medkitInHands = _player?.TryGetItemInHands<MedsItemClass>();

            // Ignora cura feita por stims e não tenta cancelar quando está consertando um membro quebrado
            if (medkitInHands != null && !_activeHealthController.IsBodyPartBroken(bodyPart))
            {
                ValueStruct bodyPartHealth = _activeHealthController.GetBodyPartHealth(bodyPart);

                // Verifica se há sangramento na parte do corpo
                // Pode haver uma forma melhor de verificar, mas isso funciona
                var effects = _activeHealthController.BodyPartEffects.Effects[bodyPart];
                bool bleeding = effects.ContainsKey("LightBleeding") || effects.ContainsKey("HeavyBleeding");

                // Verifica se o item de cura está esgotado
                // Nota: Pode não estar funcionando corretamente - o autocancel deveria disparar quando o medkit acaba
                bool healingItemDepleted = medkitInHands.MedKitComponent.HpResource < 1;

                // Cancela a cura se:
                // 1. A parte do corpo está no máximo de saúde E não está sangrando, OU
                // 2. O item de cura está esgotado
                if ((bodyPartHealth.AtMaximum && !bleeding) || healingItemDepleted)
                {
                    // Esta é a parte mágica! Cancela o efeito de cura
                    _activeHealthController.RemoveMedEffect();
                }
            }
        }
    }
}

