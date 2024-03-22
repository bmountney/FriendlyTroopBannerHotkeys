using System;
//using TaleWorlds.Core;
using TaleWorlds.Library;

namespace FriendlyTroopBannerHotkeys
{
    class Utility
    {
        public static void LogMessage(string message, string c = "#ffffffff")
        {
            InformationManager.DisplayMessage(new InformationMessage(message, Color.ConvertStringToColor(c)));
            //Debug.Print(s, 0, Debug.DebugColor.White, 17592186044416UL);
        }

        public static void Log(string message, string c = "#ffffffff")
        {
            LogMessage(FriendlyTroopBannerHotkeys.FriendlyModName + " - " + message, c);
        }

        public static void LogVerbose(string message, string c = "#ffffffff")
        {
            if (FriendlyTroopBannerHotkeys.Settings.VerboseLog)
            {
                Log(message, c);
            }
        }

        public static void LogDebug(string method, string message)
        {
            if (FriendlyTroopBannerHotkeysModSettings.Debug)
            {
                Log($"{method} debug: {message}");
            }
        }

        public static void Log(Exception ex)
        {
            Log($"exception: {ex.Message}", "#b51705FF");
        }

        public static void Log(String method, Exception ex)
        {
            Log($"{method} exception: {ex.Message}", "#b51705FF");
        }
    }
}
