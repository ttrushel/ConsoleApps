namespace ClientConsoleApp.Helpers
{
    public interface ILoggerManager
    {
        void LogInformation(string message);
        void Log(string message, LogType type);
    }
}
