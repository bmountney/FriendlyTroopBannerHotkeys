﻿using System;
using System.IO;
using System.Xml.Serialization;
using TaleWorlds.InputSystem;

namespace FriendlyTroopBannerHotkeys
{
    public class FriendlyTroopBannerHotkeysModSettings
    {
        // This code in this class is largely based on the settings file system in Wesir54's PartyManager mod
        // (https://github.com/data54/BannerlordMods) and I appreciate that having been made avaialble on Github
        // as it saved me a good amount of time in adding this feature to my mod, since I wasn't up to speed
        // on C# serialization, most of my preivous programming experience having been years ago in C++.

        const string setttingsPathName = "..\\..\\Modules\\" + FriendlyTroopBannerHotkeys.ModName + "\\ModuleData\\" + FriendlyTroopBannerHotkeys.ModName + "ModSettings.xml";
        const string hotkeyInstructions = "Choose from any of the labels in the \"KeyIdentifiers\" list to specify a key binding in the \"MomentaryBannerToggleKey\" or \"StickyBannerToggleKey\" settings. If the \"UseGameShowIndicatorsBindingForMomentary\" setting is set to \"true\", then the \"MomemtaryBannerToggleHotkey\" will be ignored, and it will instead use whatever key is bound to \"Show Indicators\" in the game settings. In any case where a key is bound to functions in both the mod and the game, it will then perform both the mod function and the game function simultaneously. The names are case sensitive, and must be entered exactly as listed. The \"D1\" through \"D0\" keys are the numeric keys on the main keyboard, and I think the rest should be self-explanatory.";
        const string bannerScalingInstructions = "The \"BannerScaleFactor\" option controls the normal banner size, and can be set from 0.1, which is 1/10th normal size, up to 1.0, which would be normal size.  The \"SelectedBannerScaleFactor\" controls the size of the banners with the outer yellow circle for selected troops.  This scaling works differently than the normal banner size, and unfortunately can never be made smaller than the original size.  Instead, it controls the extra enlargement of selected troop banners that occurs with more distant troops, to make it easier to see the selected troops when they're farther away.  The default value is 30, and this will result in the selected banners being enlarged by the usual amount.  It can be lowered down to 1, which will result in the distant banners not being enlarged at all, or increased to 60, which will enlarge them twice as much as usual.  Values lower than 1 have no effect, so the selected troop banners can never be made smaller than the original size.  (I find that a value of 0.5 for the \"BannerScaleFactor\" and 15 for the \"SelectedBannerScaleFactor\" seems to work pretty well for making the banners less obtrusive.)  Since the patches required to achieve the banner scaling are more invasive and more likely to break with future game updates, I added another config option called \"ApplyBannerScalingMod\" that must be changed to \"true\" for banner scaling to happen at all.  If the banner scaling causes a future version of the game to crash or throw an exception, you can change setting back to \"false\" and then the banner scaling patches will not be applied at all.";
        const string keyIdentifiers = "Escape, D1, D2, D3, D4, D5, D6, D7, D8, D9, D0, Minus, Equals, BackSpace, Tab, Q, W, E, R, T, Y, U, I, O, P, OpenBraces, CloseBraces, Enter, LeftControl, A, S, D, F, G, H, J, K, L, SemiColon, Apostrophe, Tilde, LeftShift, BackSlash, Z, X, C, V, B, N, M, Comma, Period, Slash, RightShift, NumpadMultiply, LeftAlt, Space, CapsLock, F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, Numpad7, Numpad8, Numpad9, NumpadMinus, Numpad4, Numpad5, Numpad6, NumpadPlus, Numpad1, Numpad2, Numpad3, Numpad0, NumpadPeriod, Extended, F11, F12, NumpadEnter, RightControl, NumpadSlash, RightAlt, NumLock, Home, Up, PageUp, Left, Right, End, Down, PageDown, Insert, Delete, ControllerLStick, ControllerRStick, LeftMouseButton, RightMouseButton, MiddleMouseButton, X1MouseButton, X2MouseButton, MouseScrollUp, MouseScrollDown, ControllerLStickUp, ControllerLStickDown, ControllerLStickLeft, ControllerLStickRight, ControllerRStickUp, ControllerRStickDown, ControllerRStickLeft, ControllerRStickRight, ControllerLUp, ControllerLDown, ControllerLLeft, ControllerLRight, ControllerRUp, ControllerRDown, ControllerRLeft, ControllerRRight, ControllerLBumper, ControllerRBumper, ControllerLOption, ControllerROption, ControllerLThumb, ControllerRThumb, ControllerLTrigger, ControllerRTrigger";
        const int settingsVersion = 2;

        public int? SettingsVersion { get; set; }
        public bool UseGameShowIndicatorsBindingForMomentary { get; set; }
        public InputKey MomentaryBannerToggleHotkey { get; set; }
        public InputKey StickyBannerToggleHotkey { get; set; }
        public bool ApplyBannerScalingMod { get; set; }
        public float BannerScaleFactor { get; set; }
        public float SelectedBannerScaleFactor { get; set; }
        public bool Debug { get; set; }
        public string HotkeyInstructions { get; set; }
        public string KeyIdentifiers { get; set; }
        public string BannerScalingInstructions { get; set; }

        [XmlIgnore]
        private static XmlSerializer Serializer
        {
            get
            {
                if (_serializer == null)
                {
                    _serializer = new XmlSerializer(typeof(FriendlyTroopBannerHotkeysModSettings));
                }
                return _serializer;
            }
        }
        static XmlSerializer _serializer;

        [XmlIgnore]
        public string NewFileMessage = null;

        private static FriendlyTroopBannerHotkeysModSettings _settings;

        [XmlIgnore]
        public static FriendlyTroopBannerHotkeysModSettings Settings
        {
            get
            {
                if (_settings == null)
                {
                    _settings = LoadSettings();
                }
                return _settings;
            }
            set
            {
                _settings = value;
            }
        }

        public FriendlyTroopBannerHotkeysModSettings()
        {
            SettingsVersion = settingsVersion;
            UseGameShowIndicatorsBindingForMomentary = true;
            MomentaryBannerToggleHotkey = InputKey.LeftAlt;
            StickyBannerToggleHotkey = InputKey.RightAlt;
            ApplyBannerScalingMod = false;
            BannerScaleFactor = 1.0f;
            SelectedBannerScaleFactor = 30.0f;
            Debug = false;
            HotkeyInstructions = hotkeyInstructions;
            BannerScalingInstructions = bannerScalingInstructions;
            KeyIdentifiers = keyIdentifiers;
        }

        public static FriendlyTroopBannerHotkeysModSettings LoadSettings()
        {
            if (!File.Exists(setttingsPathName))
            {
                return InitializeFile();
            }
            else
            {
                FriendlyTroopBannerHotkeysModSettings settings;
                bool needToSaveFile = false;

                using (FileStream fs = new FileStream(setttingsPathName, FileMode.Open))
                {
                    try
                    {
                        settings = Serializer.Deserialize(fs) as FriendlyTroopBannerHotkeysModSettings;
                    }
                    catch (Exception ex)
                    {
                        Utility.Log("LoadSettings", ex);
                        settings = new FriendlyTroopBannerHotkeysModSettings();
                        needToSaveFile = true;
                    }
                }

                if (settings.HotkeyInstructions != hotkeyInstructions) { settings.HotkeyInstructions = hotkeyInstructions; }
                if (settings.BannerScalingInstructions != bannerScalingInstructions) { settings.BannerScalingInstructions = bannerScalingInstructions; }

                // Check for valid option ranges and adjust if necessary
                if (settings.BannerScaleFactor < 0.1f) { settings.BannerScaleFactor = 0.1f; needToSaveFile = true; }
                if (settings.BannerScaleFactor > 1f) { settings.BannerScaleFactor = 1f; needToSaveFile = true; }
                if (settings.SelectedBannerScaleFactor < 1f) { settings.SelectedBannerScaleFactor = 1f; needToSaveFile = true; }
                if (settings.SelectedBannerScaleFactor > 60f) { settings.SelectedBannerScaleFactor = 60f; needToSaveFile = true; }

                if (needToSaveFile || settings.SettingsVersion != settingsVersion)
                {
                    settings = InitializeFile(settings);
                }

                return settings;
            }
        }

        public static void ReloadSettings()
        {
            _settings = LoadSettings();
            Utility.Log("Settings Reloaded");
        }

        public void SaveSettings()
        {
            InitializeFile(this);
            Utility.LogDebug("Settings", "Settings Saved");
        }

        private static FriendlyTroopBannerHotkeysModSettings InitializeFile(FriendlyTroopBannerHotkeysModSettings settings = null)
        {
            try
            {
                var fileCreated = false;
                if (settings == null)
                {
                    settings = new FriendlyTroopBannerHotkeysModSettings();
                    fileCreated = true;
                }
                else
                {
                    if (settings.SettingsVersion != settingsVersion || settings.HotkeyInstructions != hotkeyInstructions)
                    {
                        settings.HotkeyInstructions = hotkeyInstructions;
                        settings.SettingsVersion = settingsVersion;
                    }
                }

                var settingsPath = Path.GetDirectoryName(setttingsPathName);
                if (!Directory.Exists(settingsPath))
                {
                    Directory.CreateDirectory(settingsPath);
                }

                using (var fs = new FileStream(setttingsPathName, FileMode.Create))
                {
                    Serializer.Serialize(fs, settings);
                }

                if (fileCreated)
                {
                    var fullPath = Path.GetFullPath(setttingsPathName);
                    settings.NewFileMessage = FriendlyTroopBannerHotkeys.ModName + $" config generated at {fullPath}";
                }
            }
            catch (Exception ex)
            {
                Utility.Log("FriendlyTroopBannerHotkeysModSettings.InitializeFile", ex);
            }

            return settings;
        }

        public FriendlyTroopBannerHotkeysModSettings Clone()
        {
            try
            {
                FriendlyTroopBannerHotkeysModSettings clone;
                using (MemoryStream ms = new MemoryStream())
                {
                    Serializer.Serialize(ms, this);
                    ms.Seek(0, SeekOrigin.Begin);
                    clone = Serializer.Deserialize(ms) as FriendlyTroopBannerHotkeysModSettings;
                }

                return clone;
            }
            catch (Exception ex)
            {
                Utility.Log("FriendlyTroopBannerHotkeysModSettings.Clone", ex);
            }

            return null;
        }
    }
}
