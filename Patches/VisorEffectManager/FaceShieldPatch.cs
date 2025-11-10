using System.Reflection;
using SPT.Reflection.Patching;
using UnityEngine;

namespace JeroManyMods.Patches.VisorEffectManager
{
    internal class FaceShieldPatch : ModulePatch
    {

        protected override MethodBase GetTargetMethod()
        {
            return typeof(VisorEffect).GetMethod("method_2", BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
        }

        [PatchPostfix]
        static void Postfix(VisorEffect __instance)
        {
            try
            {
                // Verifica se a instância é válida
                if (__instance == null)
                {
                    MainJeroManyMods.Logger?.LogWarning("FaceShieldPatch: VisorEffect instance is null");
                    return;
                }

                // Obtém o material
                Material material = __instance.method_4();

                // Verifica se o material é válido antes de aplicar configurações
                if (material == null)
                {
                    MainJeroManyMods.Logger?.LogWarning("FaceShieldPatch: Material is null, skipping configuration application");
                    return;
                }

                // Aplica configurações apenas se as entradas de configuração estiverem inicializadas
                if (MainJeroManyMods.RemoveGlassDamage == null ||
                    MainJeroManyMods.RemoveScratches == null ||
                    MainJeroManyMods.RemoveBlur == null ||
                    MainJeroManyMods.RemoveDistortion == null)
                {
                    MainJeroManyMods.Logger?.LogWarning("FaceShieldPatch: Configuration entries not initialized yet");
                    return;
                }

                // Remove texturas baseado nas configurações
                if (MainJeroManyMods.RemoveGlassDamage.Value)
                {
                    material.SetTexture("_GlassDamageTex", null);
                }

                if (MainJeroManyMods.RemoveScratches.Value)
                {
                    material.SetTexture("_ScratchesTex", null);
                }

                if (MainJeroManyMods.RemoveBlur.Value)
                {
                    material.SetTexture("_BlurMask", null);
                }

                if (MainJeroManyMods.RemoveDistortion.Value)
                {
                    material.SetTexture("_DistortMask", null);
                }

                MainJeroManyMods.Logger?.LogDebug($"FaceShieldPatch: Applied settings to visor effect (Material: {material.name})");
            }
            catch (System.Exception ex)
            {
                MainJeroManyMods.Logger?.LogError($"FaceShieldPatch: Error applying visor settings - {ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}