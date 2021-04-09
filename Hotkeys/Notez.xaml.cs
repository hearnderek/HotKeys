using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hotkeys
{
    /// <summary>
    /// TestWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class NotezWindow : Window
    {
        public string SaveFile = Conf.noteFile;

        public DateTime Lag = DateTime.MinValue;
        bool Updating = false;
        public NotezWindow()
        {
            InitializeComponent();

            if (System.IO.File.Exists(SaveFile))
            {
                var tr = new TextRange(MainText.Document.ContentStart, MainText.Document.ContentEnd);
                using (var fs = new System.IO.FileStream(SaveFile, System.IO.FileMode.Open))
                {
                    tr.Load(fs, System.Windows.DataFormats.Rtf);
                }
            }

            this.KeyUp += HandleKeyUp;
            this.MainText.PreviewMouseLeftButtonUp += HandleMouseButtonUp;
        }

        private void HandleMouseButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Lag <= DateTime.Now)
            {
                Lag = DateTime.Now.AddMilliseconds(Conf.lagTypingUpdate);
                Updating = true;
                TextAddins.UpdateDocument(MainText);
                Updating = false;
            }
        }

        private void HandleKeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape)
            {
                this.Close();
            }

            if (Lag <= DateTime.Now)
            {
                Lag = DateTime.Now.AddMilliseconds(Conf.lagTypingUpdate);
                Updating = true;
                TextAddins.UpdateDocument(MainText);
                Updating = false;
            }
        }
    }
}
