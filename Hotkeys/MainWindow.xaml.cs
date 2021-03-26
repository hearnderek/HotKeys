using System;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.ComponentModel;

namespace Hotkeys
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        // For registering hotkeys and their actions
        public HashSet<Key> KeysWeCareAbout;
        public List<KeysAndAction> hotKeys;
        public HashSet<Key> keysDown = new HashSet<Key>();
        public MainLoop looper;


        // For writing out output
        DateTime lag = DateTime.Now;
        bool activeInputBox = false;
        string logfile = @"C:\Users\derek.hearn\cmdtools\logs\Timespent.log";

        public MainWindow()
        {
            InitializeComponent();
            InitKeyCombos();
            InitText();
            InitBackgroundLoop();
            this.Closing += HandleClosing;
        }

        private void HandleClosing(object sender, CancelEventArgs e)
        {
            if(win != null)
            {
                win.Close();
            }

            if(dwin != null)
            {
                dwin.Close();
            }

            Application.Current.Shutdown();
        }


        #region Init

        private void InitText()
        {
            UpdateCenterLabel("Hit Both shift buttons");
            UpdateLastMessageLabel("Hit Both shift buttons");
        }

        private void InitBackgroundLoop()
        {
            // Pumps events to the UI to check for updates
            looper = new MainLoop(this);
            looper.OnTextUpdated += HandleUpdateText;
            looper.OnKeyCheck += HandleKeyCheck;
            looper.OnLoopTick += HandleLoopTick;
            looper.task = new Task(looper.Loop);



            //- Not needed anymore but took a moment to figure out FromCurrentSynchronizationContext
            //backgroundLoop.ContinueWith((t) => {
            //    this.WindowState = WindowState.Minimized;
            //    //Application.Current.Dispatcher.BeginInvokeShutdown(DispatcherPriority.Normal); 
            //}, TaskScheduler.FromCurrentSynchronizationContext());
            //backgroundLoop.ContinueWith((t) => {
            //    this.WindowState = WindowState.Normal;
            //}, TaskScheduler.FromCurrentSynchronizationContext());

            looper.task.Start();
        }

        private void InitKeyCombos()
        {
            this.hotKeys = new List<KeysAndAction>()
            {
                new KeysAndAction(new [] { Key.LeftShift, Key.RightShift }, AskWorkUpdate),
                new KeysAndAction(new [] { Key.LeftCtrl, Key.RightCtrl }, OpenTestWindow)
            };
            this.KeysWeCareAbout = new HashSet<Key>(hotKeys.SelectMany(x => x.keys));
        }

        #endregion

        #region UI Updates

        public void UpdateLastMessageLabel(string txt)
        {
            this.lastMessageLabel.Content = txt;
        }

        /// <summary>
        /// Ultimately used by the updateText handler, but also used within the class as well
        /// </summary>
        public void UpdateCenterLabel(string txt)
        {
            this.centerLabel.Content = txt;
        }

        #endregion

        #region Handlers

        /// <summary>
        /// What the background worker will be invoking via the dispatcher
        /// </summary>
        public void HandleUpdateText(object sender, TextEventArgs txt)
        {
            UpdateCenterLabel(txt.txt);
        }

        private void HandleKeyCheck(object sender, KeyCheckEventArgs e)
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


        private void HandleLoopTick(object sender, LoopTickEventArgs e)
        {

        }
        #endregion


        public void CheckKeys()
        {
            HashSet<Key> keysDown = new HashSet<Key>(KeysWeCareAbout.Where(key => Keyboard.IsKeyDown(key)));
            foreach(KeysAndAction hotKey in hotKeys)
            {
                if(hotKey.keys.All(k => keysDown.Contains(k)))
                {
                    hotKey.action();
                }
            }
        }

        public void AskWorkUpdate()
        {
            if (DateTime.Now > lag && !activeInputBox)
            {
                lag = DateTime.Now.AddMilliseconds(1000);
                activeInputBox = true;
                // It would be nice to replace this with my own custom input box, but that is kind of a waste of time
                string answer = Microsoft.VisualBasic.Interaction.InputBox("What are you working on?", "Time Logger", "");
                activeInputBox = false;

                if (!String.IsNullOrWhiteSpace(answer))
                {
                    string now = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    string line = now + " - " + answer;
                    System.IO.File.AppendAllLines(logfile, new[] { line });
                    UpdateLastMessageLabel(line);
                }


                // Minimize and Maximize buttons were removed by setting ResizeMode="NoResize" within the window XAML def
                //
                //if (this.IsVisible)
                //{
                //    this.Hide();
                //} else
                //{
                //    this.Show();
                //}
            }
        }

        public Notez win = null;
        public DisplayWindow dwin = null;

        public bool shown = false;
        public void OpenTestWindow()
        {
            if (DateTime.Now > lag)
            {
                lag = DateTime.Now.AddMilliseconds(1000);
                if (win == null) 
                { 
                    win = new Notez();
                    win.Show();
                    win.Activate();
                    win.mTB.Focus();

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
                        win.mTB.Focus();
                    }
                }

                if (dwin == null)
                {
                    dwin = new DisplayWindow();
                    dwin.BindedWindow = win;
                    looper.OnLoopTick += dwin.HandleLoopTick;
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
            win = null;
            dwin?.Close();
            dwin = null;
        }
    }

}