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
            foreach(Paragraph p in AsParagraphs(doc))
            {
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
            if(p != null)
            {
                TextRange tr = new TextRange(p.ContentStart, p.ContentEnd);
                tr.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.White);
            }
        }
    }
}
