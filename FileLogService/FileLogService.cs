using System.IO;
using Ninject_FakeItEasyDemo.Infrastructure;

namespace FileLoging
{
    public class FileLogService : ILogService
    {
        private readonly string _logFilePath;

        public FileLogService(string logFilePath)
        {
            _logFilePath = logFilePath;
        }

        public void Log(string message)
        {
            File.AppendAllLines(_logFilePath, new[] { message });
        }
    }
}
