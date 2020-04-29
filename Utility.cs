using System;
using System.Reflection;
using TaleWorlds.Library;
using HarmonyLib;

namespace FriendlyTroopBannerHotkeys
{
    class Utility
    {
        public static void Log(string s)
        {
            Debug.Print(s, 0, Debug.DebugColor.White, 17592186044416UL);
        }
    }
}
