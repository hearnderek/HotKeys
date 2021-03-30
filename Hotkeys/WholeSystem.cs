using System.Threading.Tasks;

namespace Hotkeys
{
    public static class WholeSystem
    {
        public static void Initialize()
        {
            CreateAppFolder();
            SetConfiguration();

            Singletons.notezPopupController = new NotezPopupController();
            Singletons.workUpdateController = new WorkUpdateController();
            Singletons.configController = new ConfigController();
            Singletons.hotkeys = new GlobalHotKeys();

            InitBackgroundLoop(Singletons.mainWindow);
        }

        private static void InitBackgroundLoop(MainWindow mainWindow)
        {
            // Connects the background with the main window so we have an event pump
            Singletons.backgroudLoop = new MainLoop(mainWindow);
            Singletons.backgroudLoop.OnTextUpdated += mainWindow.HandleUpdateText;
            Singletons.backgroudLoop.OnKeyCheck += Singletons.hotkeys.HandleKeyCheck;
            
            // Get loop running in a background thread
            Singletons.backgroudLoop.task = new Task(Singletons.backgroudLoop.Loop);
            Singletons.backgroudLoop.task.Start();
        }

        private static void SetConfiguration()
        {
            if(System.IO.File.Exists(Conf.confFile))
            {
                Conf.LoadConfigurationFromDisk();
            }
        }

        private static void CreateAppFolder()
        {
            if (!System.IO.Directory.Exists(Conf.baseFolder))
            {
                System.IO.Directory.CreateDirectory(Conf.baseFolder);
            }
        }
    }

}