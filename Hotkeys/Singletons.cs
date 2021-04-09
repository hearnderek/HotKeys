namespace Hotkeys
{
    /// <summary>
    /// All of these objects are to be created once,
    /// </summary>
    public static class Singletons
    {
        public static MainWindow mainWindow;
        public static NotezWindow notezWindow;
        public static WorkUpdateWindow workUpdateWindow;
        public static TimespentWindow timespentWindow;
        public static DebugNotezWindow debugNotezWindow;
        public static ConfigWindow configWindow;

        public static NotezPopupController notezPopupController;
        public static WorkUpdateController workUpdateController;
        public static ConfigController configController;

        public static GlobalHotKeys hotkeys;
        public static MainLoop backgroudLoop;

    }

}