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

namespace Hotkeys
{
    /// <summary>
    /// TestWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class TestWindow : Window
    {
        public string SaveFile = "s.rtf";

        public TestWindow()
        {
            InitializeComponent();

            if (System.IO.File.Exists(SaveFile))
            {
                var tr = new TextRange(mTB.Document.ContentStart, mTB.Document.ContentEnd);
                using (var fs = new System.IO.FileStream(SaveFile, System.IO.FileMode.Open))
                {
                    tr.Load(fs, System.Windows.DataFormats.Rtf);
                }
            }
        }
    }
}
