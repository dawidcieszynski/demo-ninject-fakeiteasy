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
        private readonly string _storageFilePath;

        public LocalStorageRepository(string storageFilePath)
        {
            _storageFilePath = storageFilePath;
        }

        public void Set(string name, string value)
        {
            var settingsFileContent = string.Empty;
            if (File.Exists(_storageFilePath))
                settingsFileContent = File.ReadAllText(_storageFilePath);
            var settingsList = JsonConvert.DeserializeObject<List<SettingItem>>(settingsFileContent) ?? new List<SettingItem>();

            var item = settingsList.FirstOrDefault(s => s.Name == name);
            if (item == null)
                settingsList.Add(new SettingItem { Name = name, Value = value });
            else
                item.Value = value;

            var dir = Path.GetDirectoryName(_storageFilePath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            File.WriteAllText(_storageFilePath, JsonConvert.SerializeObject(settingsList));
        }

        public string Get(string name)
        {
            var content = string.Empty;
            if (File.Exists(_storageFilePath))
                content = File.ReadAllText(_storageFilePath);
            var deserialized = JsonConvert.DeserializeObject<List<SettingItem>>(content) ?? new List<SettingItem>();
            return deserialized.Where(s => s.Name == name).Select(s => s.Value).FirstOrDefault();
        }
    }
}
