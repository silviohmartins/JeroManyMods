using System.Reflection;
using JeroManyMods.Patches.ContinuousLoadAmmo.Components;
using JeroManyMods.Patches.ContinuousLoadAmmo.Controllers;
using EFT;
using SPT.Reflection.Patching;

namespace JeroManyMods.Patches.ContinuousLoadAmmo.Patches
{
    public class RegisterPlayerPatch : ModulePatch
    {
        private static LoadAmmoUI _loadAmmoUI;

        protected override MethodBase GetTargetMethod()
        {
            _loadAmmoUI = new LoadAmmoUI();
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

            if (iPlayer is Player player)
            {
                var loadAmmoController = new LoadAmmoController(player);
                LoadAmmoComponent.Create(player.gameObject, loadAmmoController);
                _loadAmmoUI.Initialize(loadAmmoController);

                MainJeroManyMods.Logger.LogInfo($"Added LoadAmmoComponent to player: {player.Profile.Nickname}");
                return;
            }
            MainJeroManyMods.Logger.LogError($"Unable to add LoadAmmoComponent to player: {iPlayer.Profile.Nickname}");
        }
    }
}