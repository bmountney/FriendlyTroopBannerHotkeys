using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;
using MCM.Common;
using TaleWorlds.InputSystem;

namespace FriendlyTroopBannerHotkeys
{
    public class FriendlyTroopBannerHotkeysModSettings : AttributeGlobalSettings<FriendlyTroopBannerHotkeysModSettings>
    {
        public override string Id => "FriendlyTroopBannerHotkeys";

        public override string DisplayName => "Friendly Troop Banner Hotkeys";

        public override string FolderName => "FriendlyTroopBannerHotkeys";

        public override string FormatType => "json2";

        public static bool Debug = false;
        
        //const string settingsPathName = "..\\..\\Modules\\" + FriendlyTroopBannerHotkeys.ModName + "\\ModuleData\\" + FriendlyTroopBannerHotkeys.ModName + "ModSettings.xml";
        //const string hotkeyInstructions = "Choose from any of the labels in the \"KeyIdentifiers\" list to specify a key binding in the \"MomentaryBannerToggleKey\" or \"StickyBannerToggleKey\" settings. If the \"UseGameShowIndicatorsBindingForMomentary\" setting is set to \"true\", then the \"MomemtaryBannerToggleHotkey\" will be ignored, and it will instead use whatever key is bound to \"Show Indicators\" in the game settings. In any case where a key is bound to functions in both the mod and the game, it will then perform both the mod function and the game function simultaneously. The names are case sensitive, and must be entered exactly as listed. The \"D1\" through \"D0\" keys are the numeric keys on the main keyboard, and I think the rest should be self-explanatory.";
        //const string bannerScalingInstructions = "The \"BannerScaleFactor\" option controls the normal banner size, and can be set from 0.1, which is 1/10th normal size, up to 1.0, which would be normal size.  The \"SelectedBannerScaleFactor\" controls the size of the banners with the outer yellow circle for selected troops.  This scaling works differently than the normal banner size, and unfortunately can never be made smaller than the original size.  Instead, it controls the extra enlargement of selected troop banners that occurs with more distant troops, to make it easier to see the selected troops when they're farther away.  The default value is 30, and this will result in the selected banners being enlarged by the usual amount.  It can be lowered down to 1, which will result in the distant banners not being enlarged at all, or increased to 60, which will enlarge them twice as much as usual.  Values lower than 1 have no effect, so the selected troop banners can never be made smaller than the original size.  (I find that a value of 0.5 for the \"BannerScaleFactor\" and 15 for the \"SelectedBannerScaleFactor\" seems to work pretty well for making the banners less obtrusive.)  Since the patches required to achieve the banner scaling are more invasive and more likely to break with future game updates, I added another config option called \"ApplyBannerScalingMod\" that must be changed to \"true\" for banner scaling to happen at all.  If the banner scaling causes a future version of the game to crash or throw an exception, you can change setting back to \"false\" and then the banner scaling patches will not be applied at all.";
        static InputKey[] keyIdentifiers = { InputKey.LeftAlt, InputKey.RightAlt, InputKey.LeftControl, InputKey.RightControl, InputKey.LeftShift, InputKey.RightShift, InputKey.Tab, InputKey.CapsLock, InputKey.A, InputKey.B, InputKey.C, InputKey.D, InputKey.E, InputKey.F, InputKey.G, InputKey.H, InputKey.I, InputKey.J, InputKey.K, InputKey.L, InputKey.M, InputKey.N, InputKey.O, InputKey.P, InputKey.Q, InputKey.R, InputKey.S, InputKey.T, InputKey.U, InputKey.V, InputKey.W, InputKey.X, InputKey.Y, InputKey.Z, InputKey.D1, InputKey.D2, InputKey.D3, InputKey.D4, InputKey.D5, InputKey.D6, InputKey.D7, InputKey.D8, InputKey.D9, InputKey.D0, InputKey.Minus, InputKey.Equals, InputKey.BackSpace, InputKey.OpenBraces, InputKey.CloseBraces, InputKey.BackSlash, InputKey.SemiColon, InputKey.Apostrophe, InputKey.Comma, InputKey.Period, InputKey.Slash, InputKey.Space, InputKey.NumLock, InputKey.Numpad0, InputKey.Numpad1, InputKey.Numpad2, InputKey.Numpad3, InputKey.Numpad4, InputKey.Numpad5, InputKey.Numpad6, InputKey.Numpad7, InputKey.Numpad8, InputKey.Numpad9, InputKey.NumpadSlash, InputKey.NumpadMultiply, InputKey.NumpadMinus, InputKey.NumpadPlus, InputKey.NumpadPeriod, InputKey.NumpadEnter, InputKey.Up, InputKey.Left, InputKey.Right, InputKey.Down, InputKey.Insert, InputKey.Delete, InputKey.Home, InputKey.End, InputKey.PageUp, InputKey.PageDown, InputKey.Escape, InputKey.F1, InputKey.F2, InputKey.F3, InputKey.F4, InputKey.F5, InputKey.F6, InputKey.F7, InputKey.F8, InputKey.F9, InputKey.F10, InputKey.F11, InputKey.F12, InputKey.ControllerLStick, InputKey.ControllerRStick, InputKey.LeftMouseButton, InputKey.RightMouseButton, InputKey.MiddleMouseButton, InputKey.X1MouseButton, InputKey.X2MouseButton, InputKey.MouseScrollUp, InputKey.MouseScrollDown, InputKey.ControllerLStickUp, InputKey.ControllerLStickDown, InputKey.ControllerLStickLeft, InputKey.ControllerLStickRight, InputKey.ControllerRStickUp, InputKey.ControllerRStickDown, InputKey.ControllerRStickLeft, InputKey.ControllerRStickRight, InputKey.ControllerLUp, InputKey.ControllerLDown, InputKey.ControllerLLeft, InputKey.ControllerLRight, InputKey.ControllerRUp, InputKey.ControllerRDown, InputKey.ControllerRLeft, InputKey.ControllerRRight, InputKey.ControllerLBumper, InputKey.ControllerRBumper, InputKey.ControllerLOption, InputKey.ControllerROption, InputKey.ControllerLThumb, InputKey.ControllerRThumb, InputKey.ControllerLTrigger, InputKey.ControllerRTrigger };
        //const int settingsVersion = 3;

        //public int? SettingsVersion { get; set; }

        [SettingPropertyBool("{=EnableModFunctionality}Enable Mod Functionality", Order = 0, RequireRestart = false, HintText = "{=EnableModFunctionalityHint}If this is disabled, all mod functions will be turned off and the game's own banner opacity setting will be used. Enabled by default.", IsToggle = true)]
        [SettingPropertyGroup("{=Main}Enable Hotkeys", GroupOrder = 0)]
        public bool EnableModFunctionality { get; set; } = true;

        public const float BannerOpacityMin = 0.1f;
        public const float BannerOpacityMax = 1.0f;
        [SettingPropertyFloatingInteger("{=BannerOpacity}Banner Opacity", BannerOpacityMin, BannerOpacityMax, Order = 1, RequireRestart = false, HintText = "{=BannerOpacityHint}Since the mod achives toggling the visibility of the banners by changing the game's banner opacity setting, the opacity set here will be used instead when the banners are visible, ensuring that the desired banner opacity will be retained even if the banners are toggled off at game exit. Default is 100% for full opacity.")]
        [SettingPropertyGroup("{=Main}Enable Hotkeys", GroupOrder = 0)]
        public float BannerOpacity { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=OpacityNightScale}Opacity Night Scale Factor", BannerOpacityMin, BannerOpacityMax, Order = 2, RequireRestart = false, HintText = "{=OpacityNightScaleHint}This will automatically reduce the maximum opacity by the specified amount during night battles, since an opacity level that looks good during day battles will often look too bright at night. Default is 100% for no night opacity reduction.")]
        [SettingPropertyGroup("{=Main}Enable Hotkeys", GroupOrder = 0)]
        public float OpacityNightScale { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=OpacityNightStart}Night Start Time", 0.0f, 24.0f, Order = 3, RequireRestart = false, HintText = "{=OpacityNightStartHint}This is the in-game time of day to begin using the opacity night scale factor, which is specified in 24 hour decimal time (so 0 is 12:00am, 12 is 12:00pm, and 20.5 is 8:30pm) Default is 20 for 8:00pm.")]
        [SettingPropertyGroup("{=Main}Enable Hotkeys", GroupOrder = 0)]
        public float OpacityNightStart { get; set; } = 20.0f;

        [SettingPropertyFloatingInteger("{=OpacityNightEnd}Night End Time", 0.0f, 24.0f, Order = 4, RequireRestart = false, HintText = "{=OpacityNightEndHint}This is the in-game time of day to stop using the opacity night scale factor, which is specified in 24 hour decimal time (so 0 is 12:00am, 12 is 12:00pm, and 20.5 is 8:30pm) Default is 4 for 4:00am.")]
        [SettingPropertyGroup("{=Main}Enable Hotkeys", GroupOrder = 0)]
        public float OpacityNightEnd { get; set; } = 4.0f;

        [SettingPropertyBool("{=OnByDefault}Banners Are On by Default", Order = 5, RequireRestart = false, HintText = "{=OffByDefault}When this option is enabled, the banners will start out as on until you press the momentary hotkey to hide them.  The toggle hotkey actually toggles this option so that the banners will be in the last state you left them in the next battle. Enabled by default.")]
        [SettingPropertyGroup("{=Main}Enable Hotkeys", GroupOrder = 0)]
        public bool OnByDefault { get; set; } = true;

        [SettingPropertyBool("{=VerboseLog}Show Additional Log Messages", Order = 6, RequireRestart = false, HintText = "{=VerboseLog}This is mainly for use int testing the mod, but it could also be useful if submitting a bug report. Disabled by default.")]
        [SettingPropertyGroup("{=Main}Enable Hotkeys", GroupOrder = 0)]
        public bool VerboseLog { get; set; } = false;

        [SettingPropertyDropdown("{=DecreaseOpacityHotkey}Decrease Opacity Hotkey", Order = 7, RequireRestart = false, HintText = "{=DecreaseOpacityHotkeyHint}This hotkey will decrease the current opacity level of the banners. Default is the <PgDown> key.")]
        [SettingPropertyGroup("{=Main}Enable Hotkeys", GroupOrder = 0)]
        public Dropdown<InputKey> DecreaseOpacityHotkey { get; set; } = new Dropdown<InputKey>(keyIdentifiers, selectedIndex: 82);

        [SettingPropertyDropdown("{=IncreaseOpacityHotkey}Increase Opacity Hotkey", Order = 8, RequireRestart = false, HintText = "{=IncreaseOpacityHotkeyHint}This hotkey will increase the current opacity level of the banners. Default is the <PgUp> key.")]
        [SettingPropertyGroup("{=Main}Enable Hotkeys", GroupOrder = 0)]
        public Dropdown<InputKey> IncreaseOpacityHotkey { get; set; } = new Dropdown<InputKey>(keyIdentifiers, selectedIndex: 81);

        [SettingPropertyDropdown("{=NightOpacityBypassHotkey}Night Opacity Bypass Toggle Hotkey", Order = 9, RequireRestart = false, HintText = "{=NightOpacityBypassHotkeyHint}This hotkey will toggle use of the night opacity scaling during the current battle, in case some combination of lighting and weather makes it too dim even though the hour falls within the specified night range. Default is the <End> key.")]
        [SettingPropertyGroup("{=Main}Enable Hotkeys", GroupOrder = 0)]
        public Dropdown<InputKey> NightOpacityBypassHotkey { get; set; } = new Dropdown<InputKey>(keyIdentifiers, selectedIndex: 80);

        [SettingPropertyDropdown("{=ToggleHotkey}Banner Toggle Hotkey", Order = 10, RequireRestart = false, HintText = "{=ToggleHotkeyHint}This hotkey will toggle the banner visibilty only while the key is held. Default is the <Alt> key, which also displays troop infmation in the game's default hotkey settings.")]
        [SettingPropertyGroup("{=Main}Enable Hotkeys", GroupOrder = 0)]
        public Dropdown<InputKey> StickyBannerToggleHotkey { get; set; } = new Dropdown<InputKey>(keyIdentifiers, selectedIndex: 1);

        [SettingPropertyBool("{=UseCustomMomentaryHotkey}Use Custom Hotkey for Momentary Press", Order = 0, RequireRestart = false, HintText = "{=UseCustomMomentaryHotkeyHint}If this setting is enabled, you can specify a custom hotkey for momentarily changing the banner visibility. In any case where a key is bound to functions in both the mod and the game, it will then perform both the mod function and the game function simultaneously. If this is disabled then the game's hotkey for showing troop information will be used. Enabled by default.", IsToggle = true)]
        [SettingPropertyGroup("{=Main}Enable Hotkeys/Use Custom Hotkey for Momentary Press", GroupOrder = 1)]
        public bool UseCustomMomentaryHotkey { get; set; } = false;

        [SettingPropertyDropdown("{=MomentaryHotkey}Momentary Press Hotkey", Order = 1, RequireRestart = false, HintText = "{=MomentaryHotkeyHint}This hotkey will toggle the banner visibilty only while the key is held. Default is the <Alt> key, which also displays troop infmation in the game's default hotkey settings.")]
        [SettingPropertyGroup("{=Main}Enable Hotkeys/Use Custom Hotkey for Momentary Press", GroupOrder = 1)]
        public Dropdown<InputKey> MomentaryBannerToggleHotkey { get; set; } = new Dropdown<InputKey>(keyIdentifiers, selectedIndex: 0);

        //public bool ApplyBannerScalingMod { get; set; }
        //public float BannerScaleFactor { get; set; }
        //public float SelectedBannerScaleFactor { get; set; }

        //public string HotkeyInstructions { get; set; }
        //public string KeyIdentifiers { get; set; }
        //public string BannerScalingInstructions { get; set; }

        //[XmlIgnore]
        //private static XmlSerializer Serializer
        //{
        //    get
        //    {
        //        if (_serializer == null)
        //        {
        //            _serializer = new XmlSerializer(typeof(FriendlyTroopBannerHotkeysSettings));
        //        }
        //        return _serializer;
        //    }
        //}
        //static XmlSerializer _serializer;

        //[XmlIgnore]
        //public string NewFileMessage = null;

        //private static FriendlyTroopBannerHotkeysSettings _settings;

        //[XmlIgnore]
        //public static FriendlyTroopBannerHotkeysSettings Settings
        //{
        //    get
        //    {
        //        if (_settings == null)
        //        {
        //            _settings = LoadSettings();
        //        }
        //        return _settings;
        //    }
        //    set
        //    {
        //        _settings = value;
        //    }
        //}

        //public FriendlyTroopBannerHotkeysSettings()
        //{
        //    SettingsVersion = settingsVersion;
        //    UseGameShowIndicatorsBindingForMomentary = true;
        //    BannerOpacity = 0.5f;
        //    MomentaryBannerToggleHotkey = InputKey.LeftAlt;
        //    StickyBannerToggleHotkey = InputKey.RightAlt;
        //    //ApplyBannerScalingMod = false;
        //    //BannerScaleFactor = 1.0f;
        //    //SelectedBannerScaleFactor = 30.0f;
        //    Debug = false;
        //    HotkeyInstructions = hotkeyInstructions;
        //    //BannerScalingInstructions = bannerScalingInstructions;
        //    KeyIdentifiers = keyIdentifiers;
        //}

        //public static FriendlyTroopBannerHotkeysSettings LoadSettings()
        //{
        //    if (!File.Exists(settingsPathName))
        //    {
        //        return InitializeFile();
        //    }
        //    else
        //    {
        //        FriendlyTroopBannerHotkeysSettings settings;
        //        bool needToSaveFile = false;

        //        using (FileStream fs = new FileStream(settingsPathName, FileMode.Open))
        //        {
        //            try
        //            {
        //                settings = Serializer.Deserialize(fs) as FriendlyTroopBannerHotkeysSettings;
        //            }
        //            catch (Exception ex)
        //            {
        //                Utility.Log("LoadSettings", ex);
        //                settings = new FriendlyTroopBannerHotkeysSettings();
        //                needToSaveFile = true;
        //            }
        //        }

        //        if (settings.HotkeyInstructions != hotkeyInstructions) { settings.HotkeyInstructions = hotkeyInstructions; }
        //        //if (settings.BannerScalingInstructions != bannerScalingInstructions) { settings.BannerScalingInstructions = bannerScalingInstructions; }

        //        // Check for valid option ranges and adjust if necessary
        //        //if (settings.BannerScaleFactor < 0.1f) { settings.BannerScaleFactor = 0.1f; needToSaveFile = true; }
        //        //if (settings.BannerScaleFactor > 1f) { settings.BannerScaleFactor = 1f; needToSaveFile = true; }
        //        //if (settings.SelectedBannerScaleFactor < 1f) { settings.SelectedBannerScaleFactor = 1f; needToSaveFile = true; }
        //        //if (settings.SelectedBannerScaleFactor > 60f) { settings.SelectedBannerScaleFactor = 60f; needToSaveFile = true; }

        //        if (needToSaveFile || settings.SettingsVersion != settingsVersion)
        //        {
        //            settings = InitializeFile(settings);
        //        }

        //        return settings;
        //    }
        //}

        //public static void ReloadSettings()
        //{
        //    _settings = LoadSettings();
        //    Utility.Log("Settings Reloaded");
        //}

        //public void SaveSettings()
        //{
        //    InitializeFile(this);
        //    Utility.LogDebug("Settings", "Settings Saved");
        //}

        //private static FriendlyTroopBannerHotkeysSettings InitializeFile(FriendlyTroopBannerHotkeysSettings settings = null)
        //{
        //    try
        //    {
        //        var fileCreated = false;
        //        if (settings == null)
        //        {
        //            settings = new FriendlyTroopBannerHotkeysSettings();
        //            fileCreated = true;
        //        }
        //        else
        //        {
        //            if (settings.SettingsVersion != settingsVersion || settings.HotkeyInstructions != hotkeyInstructions)
        //            {
        //                settings.HotkeyInstructions = hotkeyInstructions;
        //                settings.SettingsVersion = settingsVersion;
        //            }
        //        }

        //        var settingsPath = Path.GetDirectoryName(settingsPathName);
        //        if (!Directory.Exists(settingsPath))
        //        {
        //            Directory.CreateDirectory(settingsPath);
        //        }

        //        using (var fs = new FileStream(settingsPathName, FileMode.Create))
        //        {
        //            Serializer.Serialize(fs, settings);
        //        }

        //        if (fileCreated)
        //        {
        //            var fullPath = Path.GetFullPath(settingsPathName);
        //            settings.NewFileMessage = FriendlyTroopBannerHotkeys.ModName + $" config generated at {fullPath}";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Utility.Log("FriendlyTroopBannerHotkeysModSettings.InitializeFile", ex);
        //    }

        //    return settings;
        //}

        //public FriendlyTroopBannerHotkeysSettings Clone()
        //{
        //    try
        //    {
        //        FriendlyTroopBannerHotkeysSettings clone;
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            Serializer.Serialize(ms, this);
        //            ms.Seek(0, SeekOrigin.Begin);
        //            clone = Serializer.Deserialize(ms) as FriendlyTroopBannerHotkeysSettings;
        //        }

        //        return clone;
        //    }
        //    catch (Exception ex)
        //    {
        //        Utility.Log("FriendlyTroopBannerHotkeysModSettings.Clone", ex);
        //    }

        //    return null;
        //}
    }
}
