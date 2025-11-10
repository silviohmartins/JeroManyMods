using EFT;
using SPT.Reflection.Patching;
using System.Reflection;

namespace JeroManyMods.Patches.ContinuousHealing;

internal class CH_CancelHeal_Patch : ModulePatch
{
    protected override MethodBase GetTargetMethod()
    {
        return typeof(GClass3010)
            .GetMethod(nameof(GClass3010.CancelApplyingItem));
    }

    [PatchPrefix]
    public static void Prefix(Player ___Player)
    {
        if (___Player.IsYourPlayer)
        {
            CH_EndHeal_Patch.CancelRequested = true;
        }
    }
}