using JeroManyMods.Patches.ContinuousLoadAmmo.Components;
using EFT.InputSystem;
using SPT.Reflection.Patching;
using System.Reflection;
using UnityEngine;

namespace JeroManyMods.Patches.ContinuousLoadAmmo
{
    public class TranslateCommandPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(UIInputRoot).GetMethod(nameof(UIInputRoot.TranslateCommand));
        }

        /// <summary>
        /// Blocks other input when AmmoSelector is active
        /// </summary>
        [PatchPostfix]
        protected static void Postfix(ref InputNode.ETranslateResult __result)
        {
            if (!MainJeroManyMods.InRaid) return;
            if (LoadAmmo.Inst.AmmoSelectorActive || (Input.GetKey(MainJeroManyMods.LoadAmmoHotkey.Value.MainKey) && Input.mouseScrollDelta.y != 0))
            {
                __result = InputNode.ETranslateResult.Block;
            }
        }
    }
}