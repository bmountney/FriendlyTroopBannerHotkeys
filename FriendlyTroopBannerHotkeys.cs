using HarmonyLib;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection.Emit;
using TaleWorlds.InputSystem;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View.MissionViews;
//using SandBox.View.Missions;

namespace FriendlyTroopBannerHotkeys
{
    [HarmonyPatch]
    class FriendlyTroopBannerHotkeys
    {
        public static FriendlyTroopBannerHotkeysModSettings Settings = FriendlyTroopBannerHotkeysModSettings.Instance;
        public const string ModName = "FriendlyTroopBannerHotkeys";
        public const string ModVersion = "v1.3.0";

        static bool lastMomentaryKeyPressed = false;
        static bool lastPermToggleKeyPressed = false;
        static int opacityChangeSlowdownAmount = 50;
        static int opacityChangeSlowdownCounter = 0;

        [HarmonyPostfix]
        [HarmonyPatch(typeof(MissionView), "OnMissionScreenTick")]
        public static void OnMissionScreenTickPostfixPatch(MissionView __instance, float dt)
        {
            if (Settings.EnableModFunctionality)
            {
                // Handle sticky toggle key
                if (Input.IsKeyDown(Settings.StickyBannerToggleHotkey.SelectedValue))
                {
                    if (!lastPermToggleKeyPressed)
                    {
                        lastPermToggleKeyPressed = true;
                        Settings.OnByDefault = !Settings.OnByDefault;
                    }
                }
                else if (lastPermToggleKeyPressed)
                {
                    lastPermToggleKeyPressed = false;
                }

                // Handle momentary toggle key
                bool momentaryKeyPressed = Settings.UseCustomMomentaryHotkey ?
                    Input.IsKeyDown(Settings.MomentaryBannerToggleHotkey.SelectedValue) :
                        __instance.Input.IsGameKeyDown(GenericGameKeyContext.ShowIndicators);
                if (momentaryKeyPressed != lastMomentaryKeyPressed)
                {
                    lastMomentaryKeyPressed = momentaryKeyPressed;
                }

                // Handle opacity decrease
                if (Input.IsKeyDown(Settings.DecreaseOpacityHotkey.SelectedValue))
                {
                    if (++opacityChangeSlowdownCounter > opacityChangeSlowdownAmount)
                    {
                        opacityChangeSlowdownCounter = 0;
                        Settings.BannerOpacity -= 0.01f;
                        if (Settings.BannerOpacity < FriendlyTroopBannerHotkeysModSettings.BannerOpacityMin)
                            Settings.BannerOpacity = FriendlyTroopBannerHotkeysModSettings.BannerOpacityMin;
                    }
                }

                // Handle opacity increase
                if (Input.IsKeyDown(Settings.IncreaseOpacityHotkey.SelectedValue))
                {
                    if (++opacityChangeSlowdownCounter > opacityChangeSlowdownAmount)
                    {
                        opacityChangeSlowdownCounter = 0;
                        Settings.BannerOpacity += 0.01f;
                        if (Settings.BannerOpacity > FriendlyTroopBannerHotkeysModSettings.BannerOpacityMax)
                            Settings.BannerOpacity = FriendlyTroopBannerHotkeysModSettings.BannerOpacityMax;
                    }
                }

                // Update game settings with current opacity
                float gameOpacity = (Settings.OnByDefault ? !momentaryKeyPressed : momentaryKeyPressed)
                    ? Settings.BannerOpacity : 0.0f;
                if (ManagedOptions.GetConfig(ManagedOptions.ManagedOptionsType.FriendlyTroopsBannerOpacity) != gameOpacity)
                {
                    ManagedOptions.SetConfig(ManagedOptions.ManagedOptionsType.FriendlyTroopsBannerOpacity, gameOpacity);
                }
            }
        }
    }

    //// Since these patches are more likely to break on a future game update, changed them to be applied via the Harmony manual patch method,
    //// so that they can be completely bypassed via a configuration file option.
    ////[HarmonyPatch]
    //public class MissionAgentLabelView_BannerSize_Patch
    //{
    //    //[HarmonyPostfix]
    //    //[HarmonyPatch(typeof(MissionAgentLabelView), "get__highlightedLabelScaleFactor")]
    //    public static void Postfix_get__highlightedLabelScaleFactor(ref float __result)
    //    {
    //        // This is the scale factor for the yellow-outlined banner for selected troops, which works differently from scale for
    //        // the unselected troop banners.  This scale factor is makes the banners for selected troops larger than normal scale
    //        // when they are farther away, to make it easier to see that distant troops are selected. The default value is 30, and
    //        // it seems that it can be lowered all the way down to 1, at which point this extra enlargement no longer happens.
    //        // Values lower than 1 have no effect, so it can never be made smaller than the original banner size.
    //        __result = FriendlyTroopBannerHotkeysModSettings.Settings.SelectedBannerScaleFactor;
    //    }

    //    //[HarmonyPostfix]
    //    //[HarmonyPatch(typeof(MissionAgentLabelView), "get__labelBannerWidth")]
    //    public static void Postfix_get__labelBannerWidth(ref float __result)
    //    {
    //        // This is the scale factor for the inner colored portion of the banner.
    //        if (FriendlyTroopBannerHotkeysModSettings.Settings.ApplyBannerScalingMod)
    //            __result *= FriendlyTroopBannerHotkeysModSettings.Settings.BannerScaleFactor;
    //    }

    //    //[HarmonyPostfix]
    //    //[HarmonyPatch(typeof(MissionAgentLabelView), "get__labelBlackBorderWidth")]
    //    public static void Postfix_get__labelBlackBorderWidth(ref float __result)
    //    {
    //        // This is the scale factor for the outer black border of the banner.
    //        // I think it looks better keep this a little larger relative to the
    //        // scaling of the inner portion as the scale gets smaller.
    //        if (FriendlyTroopBannerHotkeysModSettings.Settings.ApplyBannerScalingMod)
    //            __result *= FriendlyTroopBannerHotkeysModSettings.Settings.BannerScaleFactor +
    //                (1f - FriendlyTroopBannerHotkeysModSettings.Settings.BannerScaleFactor) / 100f;
    //    }

    //    //[HarmonyTranspiler]
    //    //[HarmonyPatch(typeof(MissionAgentLabelView), "InitAgentLabel")]
    //    public static IEnumerable<CodeInstruction> Transpiler_InitAgentLabel(IEnumerable<CodeInstruction> instructions)
    //    {
    //        // This adjusts the scale of the faction banner icon within the banner circle.
    //        // Since this was controlled via constants in the code, it needed to be patched
    //        // using the Harmony Transpiler method.
    //        var codes = new List<CodeInstruction>(instructions);
    //        if (FriendlyTroopBannerHotkeysModSettings.Settings.ApplyBannerScalingMod)
    //        {
    //            for (int i = 0; i < codes.Count; i++)
    //            {
    //                if (codes[i].opcode == OpCodes.Callvirt)
    //                {
    //                    String s = codes[i].operand.ToString();
    //                    if (s == "Void SetVectorArgument(Single, Single, Single, Single)")
    //                    {
    //                        float? value;

    //                        value = (codes[i - 4].operand as float?);
    //                        if (value != null)
    //                        {
    //                            // This changes the horizontal scale, which seems to be a scale divider
    //                            // instead of a multiplier.
    //                            value *= (1.0f / FriendlyTroopBannerHotkeysModSettings.Settings.BannerScaleFactor);
    //                            codes[i - 4].operand = value;
    //                            // This changes the texture horizontal UV coordinate, which needs to be
    //                            // adjusted to match the scale.
    //                            codes[i - 2].operand = (1.0f - value) / 2.0f;
    //                        }

    //                        value = (codes[i - 3].operand as float?);
    //                        if (value != null)
    //                        {
    //                            // This changes the vertical scale, which seems to be a scale divider
    //                            // instead of a multiplier.
    //                            value *= (1.0f / FriendlyTroopBannerHotkeysModSettings.Settings.BannerScaleFactor);
    //                            codes[i - 3].operand = value;
    //                            // This changes the texture vertical UV coordinate, which needs to be
    //                            // adjusted to match the scale.
    //                            codes[i - 1].operand = (1.0f - value) / 2.0f;
    //                        }

    //                        break;
    //                    }
    //                }
    //            }
    //        }
    //        return codes.AsEnumerable();
    //    }
    //}
}
