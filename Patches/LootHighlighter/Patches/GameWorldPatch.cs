using System.Reflection;
using SPT.Reflection.Patching;
using Comfort.Common;
using EFT;
using HarmonyLib;
using JeroManyMods.Patches.LootHighlighter.Components;

namespace JeroManyMods.Patches.LootHighlighter.Patches
{
    /// <summary>
    /// Patch que adiciona o componente LootHighlighterComponent ao GameWorld quando uma raid inicia.
    /// Este patch é responsável por inicializar o sistema de highlight de loot.
    /// </summary>
    public class GameWorldPatch : ModulePatch
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
        /// Adiciona o componente LootHighlighterComponent ao GameWorld para inicializar o sistema de highlight.
        /// </summary>
        [PatchPostfix]
        public static void PatchPostFix()
        {
            var gameWorld = Singleton<GameWorld>.Instance;

            if (gameWorld == null)
            {
                return;
            }

            gameWorld.gameObject.AddComponent<LootHighlighterComponent>();
        }
    }
}

