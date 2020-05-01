using System;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View.Missions;

namespace FriendlyTroopBannerHotkeys
{
    public class FriendlyTroopBannerHotkeysSubModule : MBSubModuleBase
    {
        public FriendlyTroopBannerHotkeysSubModule()
		{
			try
			{
				var harmony = new Harmony("com.mountney.bannerlord.friendlytroopbannerhotkeys");
				harmony.PatchAll(Assembly.GetExecutingAssembly());

                // Since these patches are more likely to break on game version upgrades, allow them to be completely bypassed.
                if (FriendlyTroopBannerHotkeysModSettings.Settings.ApplyBannerScalingMod)
                {
                    // For some reason the individual "GetRuntimeMethod" calls below were returning null, so I had to
                    // use "GetRuntimeMethods" and then find the correct methods from that collection.
                    var methods = typeof(MissionAgentLabelView).GetRuntimeMethods();

                    //var original = typeof(MissionAgentLabelView).GetRuntimeMethod("get__highlightedLabelScaleFactor", new Type[] { });
                    var original = methods.First(m => m.Name.Equals("get__highlightedLabelScaleFactor"));
                    var patch = typeof(MissionAgentLabelView_BannerSize_Patch).GetMethod("Postfix_get__highlightedLabelScaleFactor");
                    harmony.Patch(original, postfix: new HarmonyMethod(patch));

                    //original = typeof(MissionAgentLabelView).GetRuntimeMethod("get__labelBannerWidth", new Type[] { });
                    original = methods.First(m => m.Name.Equals("get__labelBannerWidth"));
                    patch = typeof(MissionAgentLabelView_BannerSize_Patch).GetMethod("Postfix_get__labelBannerWidth");
                    harmony.Patch(original, postfix: new HarmonyMethod(patch));

                    //original = typeof(MissionAgentLabelView).GetRuntimeMethod("get__labelBlackBorderWidth", new Type[] { });
                    original = methods.First(m => m.Name.Equals("get__labelBlackBorderWidth"));
                    patch = typeof(MissionAgentLabelView_BannerSize_Patch).GetMethod("Postfix_get__labelBlackBorderWidth");
                    harmony.Patch(original, postfix: new HarmonyMethod(patch));

                    //original = typeof(MissionAgentLabelView).GetRuntimeMethod("InitAgentLabel", new Type[] { typeof(Agent), typeof(Banner) });
                    original = methods.First(m => m.Name.Equals("InitAgentLabel"));
                    patch = typeof(MissionAgentLabelView_BannerSize_Patch).GetMethod("Transpiler_InitAgentLabel");
                    harmony.Patch(original, transpiler: new HarmonyMethod(patch));
                }
            }
			catch (Exception ex)
			{
				Utility.Log("FriendlyTroopBannerHotkeys constructor", ex);
                throw ex;
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
