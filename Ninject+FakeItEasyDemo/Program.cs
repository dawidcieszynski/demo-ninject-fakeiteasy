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


            var logic = Kernel.Get<Logic>();
            logic.Run();
        }

        private static void ConfigureKernel()
        {
            // bindowanie dwóch usług implementujących interfejs
            // konstruktory powinny oczekiwać listy ILogService, oczekiwanie tylko jednego ILogService spowoduje wyjątek
            Kernel.Bind<ILogService>().To<FileLogService>();
            Kernel.Bind<ILogService>().To<EmailLogService>();

#if DEBUG
            Kernel.Bind<IStorage>().To<LocalStorageRepository>().WithConstructorArgument("storageFileName", @"c:\Temp\Settings.txt");
#else
            Kernel.Bind<IStorage>().To<RemoteStorageRepository>().WithConstructorArgument("url", "http://naszemiejscetrzymaniadanych.pl");
#endif
        }
    }
}
