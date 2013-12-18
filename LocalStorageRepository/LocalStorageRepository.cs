using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Ninject_FakeItEasyDemo.Infrastructure;

namespace LocalStorage
{
    /// <summary>
    /// Używa Json.NET do serializacji/deserializacji i zapisuje ustawienia do pliku
    /// </summary>
    public class LocalStorageRepository : IStorage
    {
        private readonly string _storageFileName;

        public LocalStorageRepository(string storageFileName)
        {
            _storageFileName = storageFileName;
        }

        public void Set(string name, string value)
        {
            var settingsFileContent = string.Empty;
            if (File.Exists(_storageFileName))
                settingsFileContent = File.ReadAllText(_storageFileName);
            var settingsList = JsonConvert.DeserializeObject<List<SettingItem>>(settingsFileContent);

            var item = settingsList.FirstOrDefault(s => s.Name == name);
            if (item == null)
                settingsList.Add(new SettingItem { Name = name, Value = value });
            else
                item.Value = value;

            File.WriteAllText(_storageFileName, JsonConvert.SerializeObject(settingsList));
        }

        public string Get(string name)
        {
            var content = string.Empty;
            if (File.Exists(_storageFileName))
                content = File.ReadAllText(_storageFileName);
            var deserialized = JsonConvert.DeserializeObject<List<SettingItem>>(content) ?? new List<SettingItem>();
            return deserialized.Where(s => s.Name == name).Select(s => s.Value).FirstOrDefault();
        }
    }
}
