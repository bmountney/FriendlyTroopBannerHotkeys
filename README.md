# FriendlyTroopBannerHotkeys
I had a bit of a love/hate relationship with the "Show Banners On Friendly Troops" option in Bannerlord.

On the one hand, I find the little banner icons a bit immersion breaking, and would prefer to have the option turned off for that reason.

On the other hand, certain types of troops have armor that makes it harder to identify your clan or kingdom colors from the enemies', especially if you're fighting another kingdom with similar colors.  It's even worse when you are fighting a battle after dark, where it can be hard to identify your own troops at all.  Since I don't feel like going into the options each time before a battle depending on the time of day, I usually give in and leave the option turned on.

However, leaving it turned on can have other problems, even if you don't mind the icons.  When you are fighting on foot and have troops following you, such as in bandit camp missons, the other troops will often position themselves so their banner is right in your line of sight when you are trying to aim a ranged weapon.

So, what I wanted was a key I could hold to temporarily show the banner icons if they're currently turned on, or to hide them if they're currently turned off.  I also wanted another key to toggle the option to make them stay on, such as when I'm going to be fighting a whole battle in the dark, or to stay off if there's better visibility, without having to go into the settings.

This mod provides both.  By default, the key that you have bound to the "Show Indicators" function in the settings (left ALT by default) will now also momentarily toggle the banner icons.  If you have the banners turned off, holding this key will show them, along with the other things it usually does, like showing troop formation icons and highlighting objects you can pick up or interact with.  If you have the banner icons turned on, holding this key will hide them momentarily, while still performing its other functions normally.

The second function toggles the current banner setting, just as if you went into the settings and changed it.  Since Bannerlord treats the left and right ALT keys as separate keys, I chose to bind this to the right ALT key by default.  This will allow you to quickly and easily choose to make the banners stay on when you want and stay hidden when you don't, without having to go into the settings -- and you can always press the momentary key to briefly switch them the opposite way.

Update: Version 1.1.0 of the mod now allows the hotkeys to be configured to your liking by editing a config file that will be created in the mod folder the first time the mod is loaded ("FriendlyTroopBannerHotkeys\ModuleData\FriendlyTroopBannerHotkeysModSettings.xml").  The file contains instructions for how to enter the correct key IDs.  It also allows you to choose whether the momentary toggle follows the game's "Show Indicators" binding or is bound to a separate key.  For now, it only reads the file when the game first starts up, and it may overwrite it at exit, so if you want to edit the bindings you should exit the game, edit the file, and then run the game again.  I'll try to improve on that behavior in a future version.

The mod has been tested with Bannerlord beta e1.3.0, and I believe it should work fine with earlier versions, as well.
