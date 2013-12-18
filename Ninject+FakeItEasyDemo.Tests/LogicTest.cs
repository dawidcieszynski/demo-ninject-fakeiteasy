using System.Collections.Generic;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject_FakeItEasyDemo.Infrastructure;
using Ninject_FakeItEasyDemo.Infrastructure.Consts;

namespace Ninject_FakeItEasyDemo.Tests
{
    [TestClass]
    public class LogicTest
    {
        [TestMethod]
        public void TestRunReadsActionToDo()
        {
            // przygotowanie IStorage
            var fakeStorage = A.Fake<IStorage>();

            // przygotowanie testowanej klasy logiki
            var logic = new Logic(new List<ILogService>(), fakeStorage);

            // testowa akcja
            logic.Run();

            // sprawdzanie wykonania
            A.CallTo(() => fakeStorage.Get(StorageKeys.Action)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        public void TestRunSaveLastExecutionTime()
        {
            // przygotowanie IStorage
            var fakeStorage = A.Fake<IStorage>();

            // przygotowanie testowanej klasy logiki
            var logic = new Logic(new List<ILogService>(), fakeStorage);

            // testowa akcja
            logic.Run();

            // sprawdzanie wykonania
            A.CallTo(() => fakeStorage.Set(StorageKeys.Time, A<string>.Ignored)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        public void TestRunLogsInformationAboutRunningValidActions()
        {
            // przygotowanie IStorage
            var fakeStorageForSend = A.Fake<IStorage>();
            A.CallTo(() => fakeStorageForSend.Get(StorageKeys.Action)).Returns(Actions.Send);

            // przygotowanie ILogService
            var fakeLogService = A.Fake<ILogService>();

            // przygotowanie testowanej klasy logiki
            var logic = new Logic(new List<ILogService> { fakeLogService }, fakeStorageForSend);

            // testowa akcja
            logic.Run();

            // sprawdzanie wykonania
            A.CallTo(() => fakeLogService.Log(A<string>.Ignored)).MustHaveHappened(Repeated.AtLeast.Once);
        }
    }
}
