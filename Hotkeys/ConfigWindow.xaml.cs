using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hotkeys
{
    /// <summary>
    /// WorkUpdate.xaml の相互作用ロジック
    /// </summary>
    public partial class ConfigWindow : Window
    {
        public ConfigWindow()
        {
            InitializeComponent();

            this.Closing += HandleClosing;


            pbdak.Text = Conf.pasteBinDeveloperAPIKey;
            pbaun.Text = Conf.pasteBinAPIUsername;
            pbaup.Password = Conf.pasteBinAPIUserPassword;

        }

        private void WorkInput_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter || e.Key == Key.Return)
            {
                Submit();
            }

            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void Submit()
        {
            Conf.pasteBinDeveloperAPIKey = pbdak.Text;
            Conf.pasteBinAPIUsername = pbaun.Text;
            Conf.pasteBinAPIUserPassword = pbaup.Password;
            Conf.SaveConfigurationToDisk();

            this.Close();
        }

        private void HandleClosing(object sender, CancelEventArgs e)
        {
            Singletons.configWindow = null;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Submit();
        }


    }
}
