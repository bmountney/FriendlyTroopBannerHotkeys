using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace FriendlyTroopBannerHotkeys
{
    class Utility
    {
        public static void Log(string s, string c = "#ffffffff")
        {
            InformationManager.DisplayMessage(new InformationMessage(s, Color.ConvertStringToColor(c)));
            //Debug.Print(s, 0, Debug.DebugColor.White, 17592186044416UL);
        }

        public static void LogDebug(string method, string message)
        {
            if (FriendlyTroopBannerHotkeysModSettings.Settings.Debug)
            {
                InformationManager.DisplayMessage(new InformationMessage(FriendlyTroopBannerHotkeys.ModName + $"{method} debug: {message}"));
            }
        }

        public static void Log(Exception ex)
        {
            InformationManager.DisplayMessage(new InformationMessage(FriendlyTroopBannerHotkeys.ModName + $" exception: {ex.Message}", Color.ConvertStringToColor("#b51705FF")));
        }

        public static void Log(String method, Exception ex)
        {
            InformationManager.DisplayMessage(new InformationMessage(FriendlyTroopBannerHotkeys.ModName + $" {method} exception: {ex.Message}", Color.ConvertStringToColor("#b51705FF")));
        }
    }
}
