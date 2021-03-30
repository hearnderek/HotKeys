using System.Linq;
using System.Windows.Input;
using System.Collections.Generic;
using System;

namespace Hotkeys
{
    /// <summary>
    /// Handles all hotkey that we want to be active even when another program is focused.
    /// </summary>
    public class GlobalHotKeys
    {
        public HashSet<Key> KeysWeCareAbout;
        public List<KeysAndAction> hotKeys;
        public HashSet<Key> keysDown = new HashSet<Key>();

        public GlobalHotKeys()
        {
            InitKeyCombos();
        }

        /// <summary>
        /// Decoupled from the keyboard, we check to see if our set of keys has our needed combinations
        /// </summary>
        public void CheckKeys()
        {
            HashSet<Key> keysDown = new HashSet<Key>(KeysWeCareAbout.Where(key => Keyboard.IsKeyDown(key)));
            foreach (KeysAndAction hotKey in hotKeys)
            {
                if (hotKey.keys.All(k => keysDown.Contains(k)))
                {
                    hotKey.action();
                }
            }
        }

        /// <summary>
        /// All key combos are initialized within here. Remember that these are active GLOBALLY on your machine.
        /// </summary>
        public void InitKeyCombos()
        {
            // since this reaches out to our singleton class to specify, we need HotKeys to be instanciated last.
            // It would be best to make this lazy
            this.hotKeys = new List<KeysAndAction>()
            {
                new KeysAndAction(new [] { Key.LeftShift, Key.RightShift }, Singletons.workUpdateController.Ask),
                new KeysAndAction(new [] { Key.LeftCtrl, Key.RightCtrl }, Singletons.notezPopupController.OpenTestWindow)
            };
            this.KeysWeCareAbout = new HashSet<Key>(hotKeys.SelectMany(x => x.keys));
        }

        /// <summary>
        /// Through events we check to see if the keyboard has the right combination of keys down.
        /// </summary>
        public void HandleKeyCheck(object sender, KeyCheckEventArgs e)
        {
            
            keysDown.Clear();
            foreach (Key key in KeysWeCareAbout)
            {
                if (IsGlobalKeyDown(key))
                {
                    keysDown.Add(key);
                }
            }

            CheckKeys();
        }

        private static bool IsGlobalKeyDown(Key key)
        {
            // CAVEAT: When minimized we no longer see keystates
            // Interestingly enough, we can still see keystates when not visible
            return Keyboard.IsKeyDown(key);
        }
    }

}