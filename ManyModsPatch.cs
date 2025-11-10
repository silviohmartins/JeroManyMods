using System.Reflection;
using SPT.Reflection.Patching;
using Comfort.Common;
using EFT;
using HarmonyLib;

namespace JeroManyMods
{
    /// <summary>
    /// Patch que adiciona o componente JeroManyModsScripts ao GameWorld quando uma raid inicia.
    /// Este patch é responsável por inicializar os scripts do Easy Mode (EnvironmentEnjoyer e BushWhacker).
    /// </summary>
    public class ManyModsPatch : ModulePatch
    {
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
        /// Adiciona o componente JeroManyModsScripts ao GameWorld para inicializar os scripts do Easy Mode.
        /// </summary>
        [PatchPostfix]
        public static void PatchPostFix()
        {
            var gameWorld = Singleton<GameWorld>.Instance;

            if (gameWorld == null)
            {
                return;
            }

            gameWorld.gameObject.AddComponent<JeroManyModsScripts>();
        }
    }
}