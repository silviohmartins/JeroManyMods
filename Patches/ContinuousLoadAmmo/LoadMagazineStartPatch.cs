using Comfort.Common;
using JeroManyMods.Patches.ContinuousLoadAmmo.Components;
using EFT;
using SPT.Reflection.Patching;
using System.Reflection;
using System.Threading.Tasks;

namespace JeroManyMods.Patches.ContinuousLoadAmmo
{
    public class LoadMagazineStartPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(Player.PlayerInventoryController.Class1204).GetMethod(nameof(Player.PlayerInventoryController.Class1204.Start));
        }

        [PatchPostfix]
        protected static async void Postfix(Player.PlayerInventoryController.Class1204 __instance, Task<IResult> __result)
        {
            if (!MainJeroManyMods.InRaid) return;

            LoadAmmo.Inst.LoadingStart(LoadAmmo.LoadingEventType.Load, __instance, null);
            await __result;
            LoadAmmo.Inst.LoadingEnd();
        }
    }
}