﻿using System;
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
    public partial class Notez : Window
    {
        public string SaveFile = Conf.noteFile;

        public Notez()
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
        }

        private void HandleKeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape)
            {
                this.Close();
            }


            // Apply line (paragraph) highlighting
            Block bptr = MainText.Document.Blocks.FirstBlock;
            while (bptr != null)
            {
                if(bptr is Paragraph)
                {
                    Paragraph p = (Paragraph) bptr;
                    TextRange tr = new TextRange(p.ContentStart, p.ContentEnd);
                    var txt = tr.Text;

                    if (txt.EndsWith(";"))
                    {
                        tr.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Red);
                    }
                    else if (txt.EndsWith("?") || txt.Contains("[ ]"))
                    {
                        tr.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Yellow);
                    } 
                    else if (txt.EndsWith("!"))
                    {
                        tr.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Cyan);
                    }
                    else
                    {
                        tr.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.LimeGreen);
                    }
                }
                bptr = bptr.NextBlock;
            }
        }
    }
}
