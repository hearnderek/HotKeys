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


        string notefile = System.IO.Path.Combine(
            System.Environment.GetEnvironmentVariable("USERPROFILE"),
            @"cmdtools\notes\hotkeys.txt");

        public MainWindow()
        {
            Singletons.mainWindow = this;
            InitializeComponent();
            InitText();
            InitBackgroundLoop();
            this.Closing += HandleClosing;
            

        }

        private void HandleClosing(object sender, CancelEventArgs e)
        {
            Singletons.notezWindow?.Close();

            Singletons.debugNotezWindow?.Close();

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
            Singletons.backgroudLoop = new MainLoop(this);
            Singletons.backgroudLoop.OnTextUpdated += HandleUpdateText;
            Singletons.backgroudLoop.OnKeyCheck += Singletons.hotkeys.HandleKeyCheck;
            Singletons.backgroudLoop.OnLoopTick += HandleLoopTick;
            Singletons.backgroudLoop.task = new Task(Singletons.backgroudLoop.Loop);
            Singletons.backgroudLoop.task.Start();
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


        private void HandleLoopTick(object sender, LoopTickEventArgs e)
        {

        }
        #endregion

        private void Update_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Singletons.hotkeys.GetWorkUpdateMethod()();
        }

        private void Notez_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Singletons.hotkeys.GetNotezOpenMethod()();
        }
    }

}