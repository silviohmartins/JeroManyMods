using Comfort.Common;
using EFT;
using EFT.HealthSystem;
using EFT.InventoryLogic;
using HarmonyLib;
using SPT.Reflection.Patching;
using System.Reflection;
using UnityEngine;

namespace JeroManyMods.Patches.ContinuousHealing;

internal class CH_EndHeal_Patch : ModulePatch
{
    private static FieldInfo _playerField;

    public static int Animation;
    public static bool CancelRequested;

    protected override MethodBase GetTargetMethod()
    {
        _playerField = AccessTools.Field(typeof(Player.MedsController), "_player");
        return typeof(Player.MedsController.ObservedMedsControllerClass)
            .GetMethod("method_8");
    }

    [PatchPrefix]
    public static bool Prefix(Player.MedsController.ObservedMedsControllerClass __instance, IEffect effect)
    {
        if (CancelRequested)
        {
            __instance.ClearQueue();
            return true;
        }


        if (effect is not GInterface376)
        {
            return false;
        }

        Player player = (Player)_playerField.GetValue(__instance.MedsController_0);
        if (player == null)
        {
            return true;
        }

        if (!player.IsYourPlayer)
        {
            return true;
        }

        if (__instance.MedsController_0.Item is not MedKitItemClass && (!MainJeroManyMods.HealLimbs.Value || __instance.MedsController_0.Item is not MedicalItemClass))
        {
            return true;
        }

        MedsItemClass medsItem = (MedsItemClass)__instance.MedsController_0.Item;
        if (medsItem == null)
        {
            MainJeroManyMods.Logger.LogError("medsItem was null!");
            return true;
        }

        if (medsItem.MedKitComponent == null)
        {
            return true;
        }

        if (medsItem.MedKitComponent.HpResource <= 1 && medsItem.MedKitComponent.MaxHpResource < 95)
        {
            return true;
        }

        if (player.ActiveHealthController.CanApplyItem(__instance.MedsController_0.Item, EBodyPart.Common))
        {
            player.HealthController.EffectRemovedEvent -= __instance.method_8;
            float originalDelay = ActiveHealthController.GClass3008.GClass3019_0.MedEffect.MedKitStartDelay;
            ActiveHealthController.GClass3008.GClass3019_0.MedEffect.MedKitStartDelay = (float)MainJeroManyMods.HealDelay.Value;
            IEffect newEffect = player.ActiveHealthController.DoMedEffect(__instance.MedsController_0.Item, EBodyPart.Common, 1f);
            if (newEffect == null)
            {
                __instance.State = Player.EOperationState.Finished;
                __instance.MedsController_0.FailedToApply = true;
                Callback<IOnHandsUseCallback> callbackToRun = __instance.Callback_0;
                __instance.Callback_0 = null;
                callbackToRun(__instance.MedsController_0);
                ActiveHealthController.GClass3008.GClass3019_0.MedEffect.MedKitStartDelay = originalDelay;
                return false;
            }
            ;
            player.HealthController.EffectRemovedEvent += __instance.method_8;
            ActiveHealthController.GClass3008.GClass3019_0.MedEffect.MedKitStartDelay = originalDelay;

            if (MainJeroManyMods.ResetAnimation.Value && __instance.MedsController_0.Item is not MedicalItemClass)
            {
                Animation++;
                int variant = 0;
                if (__instance.MedsController_0.Item.TryGetItemComponent(out AnimationVariantsComponent animationVariantsComponent))
                {
                    variant = animationVariantsComponent.VariantsNumber;
                }

                int newAnim = (int)Mathf.Repeat((float)Animation, (float)variant);

                if (__instance.MedsController_0.FirearmsAnimator != null)
                {
                    float mult = player.Skills.SurgerySpeed.Value / 100f;
                    FirearmsAnimator animator = __instance.MedsController_0.FirearmsAnimator;
                    animator.SetUseTimeMultiplier(1f + mult);
                    animator.SetActiveParam(true, false);
                    if (animator.HasNextLimb())
                    {
                        animator.SetNextLimb(true);
                    }
                    animator.SetAnimationVariant(newAnim);
                }
            }

            return false;
        }

        return true;
    }
}