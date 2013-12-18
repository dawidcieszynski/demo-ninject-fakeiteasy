using EmailLoging;
using FileLoging;
using LocalStorage;
using Ninject;
using Ninject_FakeItEasyDemo.Infrastructure;

namespace Ninject_FakeItEasyDemo
{
    class Program
    {
        public static KernelBase Kernel = new StandardKernel();

        static void Main(string[] args)
        {
            ConfigureKernel();

            RunApplication();
        }

        private static void ConfigureKernel()
        {
            // bindowanie dwóch usług implementujących interfejs
            // konstruktory powinny oczekiwać listy ILogService, oczekiwanie tylko jednego ILogService spowoduje wyjątek
            Kernel.Bind<ILogService>()
                .To<FileLogService>()
                .WithConstructorArgument("logFilePath", @"c:\Temp\Logs.txt");
            Kernel.Bind<ILogService>()
                .To<EmailLogService>()
                .WithConstructorArgument("host", "smtp.google.com")
                .WithConstructorArgument("recipient", "someone@world.com");

#if DEBUG
            Kernel.Bind<IStorage>().To<LocalStorageRepository>().WithConstructorArgument("storageFilePath", @"c:\Temp\Settings.txt");
#else
            Kernel.Bind<IStorage>().To<RemoteStorageRepository>().WithConstructorArgument("url", "http://naszemiejscetrzymaniadanych.pl");
#endif
        }

        private static void RunApplication()
        {
            var logic = Kernel.Get<Logic>();
            logic.Run();
        }
    }
}
