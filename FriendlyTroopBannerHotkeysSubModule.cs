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
				Utility.Log("FriendlyTroopBannerHotkeys constructor", ex.Message);
			}
		}

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();

            try
            {
                Utility.Log("Loaded " + FriendlyTroopBannerHotkeys.ModName + " " + FriendlyTroopBannerHotkeys.ModVersion + " - loaded settings file v" + FriendlyTroopBannerHotkeysModSettings.Settings.SettingsVersion);
            }
            catch (Exception ex)
            {
                Utility.Log("OnBeforeInitialModuleScreenSetAsRoot", ex);
            }
        }

    }
}
