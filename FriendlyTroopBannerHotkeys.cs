using HarmonyLib;
using TaleWorlds.InputSystem;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View.Missions;

namespace FriendlyTroopBannerHotkeys
{
    [HarmonyPatch]
    class FriendlyTroopBannerHotkeys
    {
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

            // Handle permanent toggle key
            bool permToggleKeyState = Input.IsKeyDown(InputKey.RightAlt);
            if (permToggleKeyState == true && lastPermToggleKeyState == false)
            {
                lastPermToggleKeyState = true;
                initialOptionState = !initialOptionState;
                newOptionState = !lastOptionState;
            }
            else if (permToggleKeyState == false && lastPermToggleKeyState == true)
                lastPermToggleKeyState = false;

            // Handle temporary toggle key
            bool tempToggleKeyState = __instance.Input.IsGameKeyDown(GenericGameKeyContext.ShowIndicators);
            if (tempToggleKeyState != lastTempToggleKeyState)
            {
                lastTempToggleKeyState = tempToggleKeyState;
                newOptionState = initialOptionState ? !tempToggleKeyState : tempToggleKeyState;
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
