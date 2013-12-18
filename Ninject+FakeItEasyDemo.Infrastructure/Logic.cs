using System;
using System.Collections.Generic;
using Ninject_FakeItEasyDemo.Infrastructure.Consts;

namespace Ninject_FakeItEasyDemo.Infrastructure
{
    public class Logic
    {
        private readonly List<ILogService> _logServices;
        private readonly IStorage _storage;

        public Logic(List<ILogService> logServiceses, IStorage storage)
        {
            _logServices = logServiceses;
            _storage = storage;
        }

        public void Run()
        {
            var actionToDo = _storage.Get(StorageKeys.Action);

            if (actionToDo == Actions.Send)
            {
                //tutaj działanie np. wysłanie produktu do sklepu

                _logServices.ForEach(l => l.Log("Wysłano produkt"));
            }
            else if (actionToDo == Actions.Receive)
            {
                //tutaj działanie np. odebranie produktu

                _logServices.ForEach(l => l.Log("Odebrano produkt"));
            }

            _storage.Set(StorageKeys.Time, DateTime.Now.ToString());
        }
    }
}
