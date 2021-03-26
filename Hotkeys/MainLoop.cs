using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.Linq;
using System.Threading.Tasks;

namespace Hotkeys
{

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
                uiDispatcher.BeginInvoke(OnLoopTick, this, new LoopTickEventArgs());
            }
        }

    }

}