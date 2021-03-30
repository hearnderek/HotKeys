using System;
using System.Linq;
using System.Text;
using System.Windows.Input;
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

        public MainWindow()
        {
            Singletons.mainWindow = this;
            InitializeComponent();
            WholeSystem.Initialize();

            InitText();
            this.Closing += HandleClosing;
        }

        private void HandleClosing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }


        #region Init

        private void InitText()
        {
            UpdateCenterLabel("Hit Both shift buttons");
            UpdateLastMessageLabel("Hit Both shift buttons");
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


        #endregion

        private void Update_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Singletons.hotkeys.GetWorkUpdateMethod()();
        }

        private void Notez_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Singletons.hotkeys.GetNotezOpenMethod()();
        }

        private void Conf_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if(!Singletons.configController.IsWindowOpen())
            {
                Singletons.configController.OpenWindow();
            }
        }
    }

}