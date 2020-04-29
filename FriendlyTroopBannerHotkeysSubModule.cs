using System;
using HarmonyLib;
using TaleWorlds.MountAndBlade;

namespace FriendlyTroopBannerHotkeys
{
    public class FriendlyTroopBannerHotkeysSubModule : MBSubModuleBase
    {
        public FriendlyTroopBannerHotkeysSubModule()
		{
			try
			{
				var harmony = new Harmony("com.mountney.bannerlord.togglefriendlytroopbanners");
				harmony.PatchAll(typeof(FriendlyTroopBannerHotkeysSubModule).Assembly);
			}
			catch (Exception ex)
			{
				Utility.Log(ex.Message);
			}
		}
    }
}
