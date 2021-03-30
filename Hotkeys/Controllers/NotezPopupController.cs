using System;
using System.ComponentModel;

namespace Hotkeys
{
    public class NotezPopupController
    {
        public NotezWindow win = null;
        public DebugNotezWindow dwin = null;

        public bool shown = false;
        public DateTime lag = DateTime.MinValue;


        public void OpenNotezWindow()
        {
            var win = Singletons.notezWindow;
            var dwin = Singletons.debugNotezWindow;
            if (DateTime.Now > lag)
            {
                lag = DateTime.Now.AddMilliseconds(Conf.lagMilliseconds);
                if (win == null)
                {
                    win = new NotezWindow();
                    win.Show();
                    win.Activate();
                    win.MainText.Focus();

                    win.Closing += HandleTestWindowClosing;
                }
                else
                {
                    if (shown)
                        win.Hide();
                    else
                    {
                        win.Show();
                        win.Activate();
                        win.MainText.Focus();
                    }
                }

                if (dwin == null)
                {
                    dwin = new DebugNotezWindow();
                    dwin.BindedWindow = win;
                    Singletons.backgroudLoop.OnLoopTick += dwin.HandleLoopTick;
                    //dwin.Show();
                }
                else
                {
                    //if (shown)
                    //    dwin.Hide();
                    //else
                    //    dwin.Show();
                }

                shown = !shown;
            }
        }

        private void HandleTestWindowClosing(object sender, CancelEventArgs e)
        {
            Singletons.notezWindow = null;
            dwin?.Close();
            dwin = null;
        }
    }

}