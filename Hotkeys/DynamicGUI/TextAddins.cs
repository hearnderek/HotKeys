using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Controls;

namespace Hotkeys
{
    public class TextAddins
    {
        public static void UpdateDocument(RichTextBox rtb)
        {
            ColorizePass(rtb.Document);
            ColorizeCurrentParagraph(rtb);
        }

        public static void ColorizePass(FlowDocument doc)
        {
            int minimized = 0;
            foreach(Paragraph p in AsParagraphs(doc))
            {
                TextRange tr = new TextRange(p.ContentStart, p.ContentEnd);
                var txt = tr.Text;

                SetColor(tr, txt);
                CollapsePassStep(ref minimized, tr, txt);
            }
        }

        private static int CountLevel(string s)
        {
            string x = s.TrimStart();
            int count = 0;
            for (; count < x.Length && x[count] == '#'; count++);
            return count;
        }

        /// <summary>
        /// Using an int based state machine we handle collapsing text.
        /// handles our  
        /// </summary>
        /// <param name="minimized">holds the number of '#' in our minimized section, otherwise set to zero</param>
        /// <param name="tr"></param>
        /// <param name="txt"></param>
        private static void CollapsePassStep(ref int minimized, TextRange tr, string txt)
        {
            //return;
            double fontSizeProperty = Conf.normalTextSize;
            object pv = tr.GetPropertyValue(TextElement.FontSizeProperty);
            if(pv.ToString() != "{DependencyProperty.UnsetValue}")
                fontSizeProperty = (double)tr.GetPropertyValue(TextElement.FontSizeProperty);
            
            if (txt.Trim().StartsWith("#"))
            {
                int level = CountLevel(txt);
                
                if (txt.EndsWith("-"))
                {
                    minimized = Math.Max(minimized, level);
                }
                else if (minimized >= level && !txt.EndsWith("-"))
                {
                    minimized = 0;
                }

                // Update newly minified text
                if (minimized > 0 && minimized < level && Conf.minifiedTextSize != fontSizeProperty)
                {
                    tr.ApplyPropertyValue(TextElement.FontSizeProperty, Conf.minifiedTextSize);
                }
                else if (minimized == 0 && Conf.titleTextSize != fontSizeProperty)
                {
                    tr.ApplyPropertyValue(TextElement.FontSizeProperty, Conf.titleTextSize);
                }
            }
            // Update newly minified text
            else if (minimized > 0 && Conf.minifiedTextSize != fontSizeProperty)
            {
                tr.ApplyPropertyValue(TextElement.FontSizeProperty, Conf.minifiedTextSize);
            }
            // Update newly normal text
            else if (minimized <= 0 && Conf.normalTextSize != fontSizeProperty)
            {
                tr.ApplyPropertyValue(TextElement.FontSizeProperty, Conf.normalTextSize);
            }
        }

        private static void SetColor(TextRange tr, string txt)
        {


            //return;
            if (txt.EndsWith(";"))
            {
                tr.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Red);
            }
            else if (txt.EndsWith("!"))
            {
                tr.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Cyan);
            }
            else if (txt.EndsWith("-"))
            {
                tr.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Gray);
            }
            // Lower to allow above to overwrite this rule.
            else if (txt.EndsWith("?") || txt.Contains("[ ]"))
            {
                tr.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Yellow);
            }
            else
            {
                tr.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.LimeGreen);
            }
        }

        public static IEnumerable<Paragraph> AsParagraphs(FlowDocument doc)
        {
            Block bptr = doc.Blocks.FirstBlock;
            while (bptr != null)
            {
                if (bptr is Paragraph p)
                {
                    yield return p;
                }
                bptr = bptr.NextBlock;
            }
        }

        public static Paragraph GetCurrentParagraph(RichTextBox rtb)
        {
            TextPointer caretPos = rtb.CaretPosition;
            Paragraph p = caretPos.Paragraph;
            TextRange tr = new TextRange(p.ContentStart, p.ContentEnd);

            return caretPos.Paragraph;
        }

        public static void ColorizeCurrentParagraph(RichTextBox rtb)
        {
            TextPointer caretPos = rtb.CaretPosition;
            Paragraph p = caretPos.Paragraph;
            return;
            if(p != null)
            {
                TextRange tr = new TextRange(p.ContentStart, p.ContentEnd);
                tr.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.White);
            }
        }
    }
}
