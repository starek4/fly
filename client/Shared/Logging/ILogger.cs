namespace Shared.Logging
{
    public interface ILogger
    {
        void Error(string msg);

        void Info(string msg);

        void Fatal(string msg);

        void Debug(string msg);
    }
}
