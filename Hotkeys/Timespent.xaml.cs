using System;
using System.Collections.Generic;
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
using System.IO;
using System.ComponentModel;

namespace Hotkeys
{
    /// <summary>
    /// Timespent.xaml の相互作用ロジック
    /// </summary>
    public partial class TimespentWindow : Window
    {
        public TimespentWindow()
        {
            InitializeComponent();

            this.Closing += HandleClosing;

            //var lines = File.ReadLines(Conf.timespentFile);
            UpdateTextFromFile();
        }

        public void UpdateTextFromFile()
        {
            var text = string.Join(Environment.NewLine, File.ReadLines(Conf.timespentFile).Reverse());

            Doc.Blocks.Clear();
            Doc.Blocks.Add(new Paragraph(new Run(text)));

            MainText.Focus();
            MainText.ScrollToHome();
        }

        private void HandleClosing(object sender, CancelEventArgs e)
        {
            Singletons.timespentWindow = null;
        }
    }
}
