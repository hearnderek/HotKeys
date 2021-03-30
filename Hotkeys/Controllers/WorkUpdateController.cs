using System;

namespace Hotkeys
{
    public class WorkUpdateController
    {
        public struct Cache
        {
            public string LastMessageUpdate;
        }

        private DateTime lag = DateTime.MinValue;

        public Cache cache = new Cache();

        public void OpenWindow()
        {
            Singletons.workUpdateWindow = new WorkUpdateWindow();
            Singletons.workUpdateWindow.Show();
            Singletons.workUpdateWindow.Activate();
            Singletons.workUpdateWindow.WorkInput.Focus();
        }

        public bool IsWindowOpen()
        {
            return Singletons.workUpdateWindow != null;
        }

        public void Ask()
        {
            if (DateTime.Now > lag && !IsWindowOpen())
            {
                lag = DateTime.Now.AddMilliseconds(1000);
                OpenWindow();
            }
        }

        public void WriteAnswer(string answer)
        {
            if (!String.IsNullOrWhiteSpace(answer))
            {
                string now = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                string line = now + " - " + answer;

                System.IO.File.AppendAllLines(Conf.logFile, new[] { line });
                cache.LastMessageUpdate = line;
            }
        }
    }

}