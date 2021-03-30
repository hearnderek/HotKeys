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
        public static DebugNotezWindow debugNotezWindow;

        public static NotezPopupController notezPopupController = new NotezPopupController();
        public static WorkUpdateController workUpdateController = new WorkUpdateController();

        public static GlobalHotKeys hotkeys = new GlobalHotKeys();
        public static MainLoop backgroudLoop;

    }

}