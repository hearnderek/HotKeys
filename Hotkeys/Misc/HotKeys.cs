using System.Linq;
using System.Windows.Input;
using System.Collections.Generic;

namespace Hotkeys
{
    public class HotKeys
    {
        public HashSet<Key> KeysWeCareAbout;
        public List<KeysAndAction> hotKeys;
        public HashSet<Key> keysDown = new HashSet<Key>();

        public HotKeys()
        {
            InitKeyCombos();
        }

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

        public void InitKeyCombos()
        {
            this.hotKeys = new List<KeysAndAction>()
            {
                new KeysAndAction(new [] { Key.LeftShift, Key.RightShift }, Singletons.workUpdater.Ask),
                new KeysAndAction(new [] { Key.LeftCtrl, Key.RightCtrl }, Singletons.notezPopupController.OpenTestWindow)
            };
            this.KeysWeCareAbout = new HashSet<Key>(hotKeys.SelectMany(x => x.keys));
        }

        public void HandleKeyCheck(object sender, KeyCheckEventArgs e)
        {
            // When minimized we no longer see keystates
            // we can still see keystates when not visible

            keysDown.Clear();
            foreach (Key key in KeysWeCareAbout)
            {
                if (Keyboard.IsKeyDown(key))
                {
                    keysDown.Add(key);
                }
            }

            CheckKeys();
        }
    }

}