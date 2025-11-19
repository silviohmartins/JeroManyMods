using System.Reflection;
using SPT.Reflection.Patching;
using EFT.UI;
using HarmonyLib;
using UnityEngine;

namespace JeroManyMods.Patches.HideUI.Patches
{
    /// <summary>
    /// Patch para remover o aviso Alpha/Beta do menu principal.
    /// </summary>
    internal class HideBetaPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(MenuScreen), nameof(MenuScreen.method_3));
        }

        [PatchPostfix]
        static void Postfix(MenuScreen __instance, GameObject ____alphaWarningGameObject)
        {
            try
            {
                // Verifica se a configuração está habilitada
                if (MainJeroManyMods.HideBetaWarning == null || !MainJeroManyMods.HideBetaWarning.Value)
                {
                    return;
                }

                // Verifica se o objeto existe
                if (____alphaWarningGameObject == null)
                {
                    MainJeroManyMods.Logger?.LogWarning("HideBetaPatch: Alpha warning GameObject is null");
                    return;
                }

                // Desativa o objeto
                ____alphaWarningGameObject.SetActive(false);
                MainJeroManyMods.Logger?.LogDebug("HideBetaPatch: Alpha warning hidden");
            }
            catch (System.Exception ex)
            {
                MainJeroManyMods.Logger?.LogError($"HideBetaPatch: Error hiding beta warning - {ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}

