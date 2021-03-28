using System;

namespace Hotkeys
{
    public class WorkUpdater
    {
        public struct Cache
        {
            public string LastMessageUpdate;
        }

        private bool activeInputBox = false;
        private DateTime lag = DateTime.MinValue;

        public Cache cache = new Cache();

        public void Ask()
        {
            if (DateTime.Now > lag && !activeInputBox)
            {
                lag = DateTime.Now.AddMilliseconds(1000);
                activeInputBox = true;
                // It would be nice to replace this with my own custom input box, but that is kind of a waste of time
                string answer = Microsoft.VisualBasic.Interaction.InputBox("What are you working on?", "Time Logger", "");
                activeInputBox = false;

                if (!String.IsNullOrWhiteSpace(answer))
                {
                    string now = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    string line = now + " - " + answer;

                    System.IO.File.AppendAllLines(Conf.logFile, new[] { line });
                    cache.LastMessageUpdate = line;

                    //UpdateLastMessageLabel(line);
                }
            }
        }
    }

}