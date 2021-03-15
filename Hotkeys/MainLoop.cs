using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.Linq;

namespace Hotkeys
{



    public class TextEventArgs : EventArgs
    {
        public string txt { get; set; }
    }

    public class KeyCheckEventArgs : EventArgs
    {
    }

    public class MainLoop
    {
        public delegate void UpdateText(object sender, TextEventArgs txt);
        //public delegate void StartSequence(object sender, StartSequenceEventArgs e);
        public delegate void KeyCheck(object sender, KeyCheckEventArgs e);

        public event UpdateText OnTextUpdated;
        //public event StartSequence OnStartSequence;
        public event KeyCheck OnKeyCheck;

        public Dispatcher uiDispatcher;
        public DependencyObject uiDO;

        public MainLoop(Window caller)
        {
            uiDO = caller;
            this.uiDispatcher = caller.Dispatcher;
        }



        public void Loop()
        {
            long i = 0;

            while (true)
            {
                System.Threading.Thread.Sleep(100);
                uiDispatcher.BeginInvoke(OnTextUpdated, this, new TextEventArgs { txt = (i++).ToString() + " ... " + System.Threading.Thread.CurrentThread.ManagedThreadId });
                uiDispatcher.BeginInvoke(OnKeyCheck, this, new KeyCheckEventArgs());
            }
        }

    }

}