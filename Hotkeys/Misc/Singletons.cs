namespace Hotkeys
{
    /// <summary>
    /// All of these objects are to be created once,
    /// </summary>
    public static class Singletons
    {
        public static MainWindow mainWindow;
        public static Notez notezWindow;
        public static NotezPopupController notezPopupController = new NotezPopupController();


        public static Notez notez;
        public static WorkUpdater workUpdater = new WorkUpdater();
        public static HotKeys hotkeys = new HotKeys();
        public static MainLoop backgroudLoop;
        public static DebugNotezWindow debugNotezWindow;
    }

}