using System;

namespace Hotkeys
{
    public class ConfigController
    {
              
        public void OpenWindow()
        {
            Singletons.configWindow = new ConfigWindow();
            Singletons.configWindow.Show();
            Singletons.configWindow.Activate();
            Singletons.configWindow.pbdak.Focus();
        }

        public bool IsWindowOpen()
        {
            return Singletons.configWindow != null;
        }
    }

}