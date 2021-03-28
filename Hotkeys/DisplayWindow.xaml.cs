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
    public partial class DebugNotezWindow : Window
    {
        public DateTime lag = DateTime.Now;

        private Notez _bw;
        public Notez BindedWindow {
            get => _bw;
            set 
            {
                _bw = value;
                _bw.MainText.TextChanged += HandleTextChanged;
                updated = true;
            }
        }

        private bool updated = false;

        public void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            updated = true;
        }

        public DebugNotezWindow()
        {
            InitializeComponent();
        }

        public void HandleLoopTick(object sender, LoopTickEventArgs e)
        {
            if (DateTime.Now > lag && updated)
            {
                lag = DateTime.Now.AddMilliseconds(1000);
                var tr = new TextRange(BindedWindow.MainText.Document.ContentStart, BindedWindow.MainText.Document.ContentEnd);
                using (var fs = System.IO.File.Create(BindedWindow.SaveFile))
                {
                    tr.Save(fs, DataFormats.Rtf);
                }

                DisplayBlocks();

                lag = DateTime.Now.AddMilliseconds(1000);
                updated = false;
            }

            // Display "everything" in a debug window.
            
            
        }

        public void DisplayBlocks()
        {
            Block bptr = BindedWindow.MainText.Document.Blocks.FirstBlock;
            this.Content = "";
            this.Content += updated.ToString() + Environment.NewLine;
            while (bptr != null)
            {
                this.Content += bptr.ToString();
                this.Content += Environment.NewLine;
                this.Content += new TextRange(bptr.ContentStart, bptr.ContentEnd).Text;
                this.Content += Environment.NewLine;

                bptr = bptr.NextBlock;
            }
        }
    }
}
