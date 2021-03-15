using System;
using System.Windows.Input;
using System.Collections.Generic;

namespace Hotkeys
{
    public partial class MainWindow
    {
        public struct KeysAndAction
        {
            public HashSet<Key> keys;
            public Action action;

            public KeysAndAction(Key[] keys, Action action)
            {
                this.keys = new HashSet<Key>(keys);
                this.action = action;
            }
        }

    }

}