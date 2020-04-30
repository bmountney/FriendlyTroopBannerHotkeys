using HarmonyLib;
using TaleWorlds.InputSystem;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View.Missions;

namespace FriendlyTroopBannerHotkeys
{
    [HarmonyPatch]
    class FriendlyTroopBannerHotkeys
    {
        public const string ModName = "FriendlyTroopBannerHotkeys";
        public const string ModVersion = "v1.1.0";

        // Start with option state from game settings
        static bool initialOptionState = ManagedOptions.GetConfig(ManagedOptions.ManagedOptionsType.ShowBannersOnFriendlyTroops) == 1f;
        static bool lastOptionState = initialOptionState;
        static bool newOptionState = initialOptionState;
        static bool lastTempToggleKeyState = false;
        static bool lastPermToggleKeyState = false;

        [HarmonyPostfix]
        [HarmonyPatch(typeof(MissionView), "OnMissionScreenTick")]
        public static void OnMissionScreenTickPostfixPatch(MissionView __instance, float dt)
        {
            // Stay in sync with any changes to the setting made in the game settings
            bool currentOptionState = ManagedOptions.GetConfig(ManagedOptions.ManagedOptionsType.ShowBannersOnFriendlyTroops) == 1f;
            if (currentOptionState != lastOptionState)
            {
                initialOptionState = lastOptionState = newOptionState = currentOptionState;
            }

            // Handle sticky toggle key
            bool stickyToggleKeyState = Input.IsKeyDown(FriendlyTroopBannerHotkeysModSettings.Settings.StickyBannerToggleHotkey);
            if (stickyToggleKeyState == true && lastPermToggleKeyState == false)
            {
                lastPermToggleKeyState = true;
                initialOptionState = !initialOptionState;
                newOptionState = !lastOptionState;
            }
            else if (stickyToggleKeyState == false && lastPermToggleKeyState == true)
                lastPermToggleKeyState = false;

            // Handle momentary toggle key
            bool momentaryToggleKeyState = FriendlyTroopBannerHotkeysModSettings.Settings.UseGameShowIndicatorsBindingForMomentary ?
                __instance.Input.IsGameKeyDown(GenericGameKeyContext.ShowIndicators) :
                Input.IsKeyDown(FriendlyTroopBannerHotkeysModSettings.Settings.MomentaryBannerToggleHotkey);
            if (momentaryToggleKeyState != lastTempToggleKeyState)
            {
                lastTempToggleKeyState = momentaryToggleKeyState;
                newOptionState = initialOptionState ? !momentaryToggleKeyState : momentaryToggleKeyState;
            }

            // Update game settings with current option state
            if (newOptionState != lastOptionState)
            {
                lastOptionState = newOptionState;
                ManagedOptions.SetConfig(ManagedOptions.ManagedOptionsType.ShowBannersOnFriendlyTroops, newOptionState ? 1f : 0f);
            }
        }
    }
}
