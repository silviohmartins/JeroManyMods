using Comfort.Common;
using JeroManyMods.Patches.ContinuousLoadAmmo.Components;
using EFT;
using SPT.Reflection.Patching;
using System.Reflection;

namespace JeroManyMods.Patches.ContinuousLoadAmmo
{
    public class RegisterPlayerPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(GameWorld).GetMethod(nameof(GameWorld.RegisterPlayer));
        }

        [PatchPostfix]
        protected static void Postfix(IPlayer iPlayer)
        {
            if (iPlayer == null)
            {
                MainJeroManyMods.Logger.LogError("Could not add component, player was null!");
                return;
            }
            if (!iPlayer.IsYourPlayer)
            {
                return;
            }

            var mainPlayer = Singleton<GameWorld>.Instance.MainPlayer;
            mainPlayer.gameObject.AddComponent<LoadAmmo>();
            MainJeroManyMods.LoadAmmoUI.Init();
            MainJeroManyMods.Logger.LogInfo($"Added LoadAmmoComponent to player: {mainPlayer.Profile.Nickname}");
        }
    }
}