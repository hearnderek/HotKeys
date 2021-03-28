namespace Hotkeys
{
    /// <summary>
    /// Global configuration
    /// </summary>
    public static class Conf
    {
        public static string logFile = System.IO.Path.Combine(
            System.Environment.GetEnvironmentVariable("USERPROFILE"),
            @"cmdtools\logs\Timespent.log");

        public static string noteFile = System.IO.Path.Combine(
            System.Environment.GetEnvironmentVariable("USERPROFILE"),
            @"cmdtools\notes\hotkeys.txt");
        
        
        public static double lagMilliseconds = 1000;
    }

}