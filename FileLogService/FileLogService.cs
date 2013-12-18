using System.IO;
using Ninject_FakeItEasyDemo.Infrastructure;

namespace FileLoging
{
    public class FileLogService : ILogService
    {
        public void Log(string message)
        {
            File.AppendAllLines(@"c:\Log.txt", new[] { message });
        }
    }
}
