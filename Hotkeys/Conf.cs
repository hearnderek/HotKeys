using System;
using System.Xml;

namespace Hotkeys
{
    /// <summary>
    /// Global configuration
    /// </summary>
    public static class Conf
    {
        public static string baseFolder = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "hotkeys");

        public static string timespentFile = System.IO.Path.Combine(baseFolder, "Timespent.log");

        public static string noteFile = System.IO.Path.Combine(baseFolder, "Note.txt");

        public static string confFile = System.IO.Path.Combine(baseFolder, "Conf.xml");

        public static double lagMilliseconds = 1000;
        public static double lagTypingUpdate = 500;
        public static double minifiedTextSize = 2.0;
        public static double normalTextSize = 14.0;
        public static double titleTextSize = 18.0;

        // These are unset until a user manually sets them
        public static string pasteBinDeveloperAPIKey = null;
        public static string pasteBinAPIUsername = null;
        public static string pasteBinAPIUserPassword = null;

        public static void LoadConfigurationFromDisk()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Conf.confFile);
            
            Conf.pasteBinDeveloperAPIKey = doc.SelectSingleNode("/root/pbdak")?.InnerText;
            Conf.pasteBinAPIUsername = doc.SelectSingleNode("/root/pbaun")?.InnerText;
            Conf.pasteBinAPIUserPassword = doc.SelectSingleNode("/root/pbaup")?.InnerText;
        }

        public static void SaveConfigurationToDisk()
        {
            XmlDocument doc = new XmlDocument();
            Append(doc, "root", "");
            Append(doc, "pbdak", pasteBinDeveloperAPIKey);
            Append(doc, "pbaun", pasteBinAPIUsername);
            Append(doc, "pbaup", pasteBinAPIUserPassword);

            
            doc.Save(confFile);
        }

        private static void Append(XmlDocument doc, string elementName, string value)
        {
            var e = doc.CreateElement(elementName);
            e.InnerText = value;
            if (elementName != "root")
            {
                doc.FirstChild.AppendChild(e);
            }
            else
            {
                doc.AppendChild(e);
            }
        }
    }


}