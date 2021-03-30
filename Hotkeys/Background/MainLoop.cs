using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.Linq;
using System.Threading.Tasks;
using System;

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

        private DateTime StartTime = DateTime.Now;

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
                Invoke(uiDispatcher, OnTextUpdated, this, new TextEventArgs { txt = StartTime.ToString("[yyyy-MM-dd HH-mm-dd]: ") + (i++).ToString()});
                Invoke(uiDispatcher, OnKeyCheck, this, new KeyCheckEventArgs());
                Invoke(uiDispatcher, OnLoopTick, this, new LoopTickEventArgs());
            }
        }

        public void Invoke(Dispatcher d, Delegate @event, Object caller, Object @args)
        {
            if(@event != null)
                d.BeginInvoke(@event, caller, @args);
        }

    }

}