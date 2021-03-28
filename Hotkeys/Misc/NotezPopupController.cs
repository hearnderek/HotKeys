using System;
using System.ComponentModel;

namespace Hotkeys
{
    public class NotezPopupController
    {
        public Notez win = null;
        public DebugNotezWindow dwin = null;

        public bool shown = false;
        public DateTime lag = DateTime.MinValue;


        public void OpenTestWindow()
        {
            var win = Singletons.notez;
            var dwin = Singletons.debugNotezWindow;
            if (DateTime.Now > lag)
            {
                lag = DateTime.Now.AddMilliseconds(Conf.lagMilliseconds);
                if (win == null)
                {
                    win = new Notez();
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
            Singletons.notez = null;
            dwin?.Close();
            dwin = null;
        }
    }

}