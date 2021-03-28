﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.Linq;
using System.Threading.Tasks;

namespace Hotkeys
{
    /// <summary>
    /// To be run in the background to push Tick events throughout the system
    /// </summary>
    public class MainLoop
    {
        public delegate void UpdateText(object sender, TextEventArgs txt);
        public delegate void KeyCheck(object sender, KeyCheckEventArgs e);
        public delegate void LoopTick(object sender, LoopTickEventArgs e);

        public event UpdateText OnTextUpdated;
        public event KeyCheck OnKeyCheck;
        public event LoopTick OnLoopTick;

        public Dispatcher uiDispatcher;
        public DependencyObject uiDO;
        public Task task;

        /// <summary>
        /// Construct object with a callback to the origional window so messages can be dispatched to the correct location
        /// </summary>
        /// <param name="caller"></param>
        public MainLoop(Window caller)
        {
            uiDO = caller;
            this.uiDispatcher = caller.Dispatcher;
        }

        /// <summary>
        /// Pumps ticks every 100 milliseconds to the system
        /// </summary>
        public void Loop()
        {
            long i = 0;

            while (true)
            {
                System.Threading.Thread.Sleep(100);
                uiDispatcher.BeginInvoke(OnTextUpdated, this, new TextEventArgs { txt = (i++).ToString() + " ... " + System.Threading.Thread.CurrentThread.ManagedThreadId });
                uiDispatcher.BeginInvoke(OnKeyCheck, this, new KeyCheckEventArgs());
                uiDispatcher.BeginInvoke(OnLoopTick, this, new LoopTickEventArgs());
            }
        }

    }

}