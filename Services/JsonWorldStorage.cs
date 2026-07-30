using System.Text.Json;
using WorldMap2026.Model;

namespace WorldMap2026.Services
{
    /// <summary>
    /// Сервис для сохранения и загрузки состояния игрового мира в формате JSON.
    /// </summary>
    public class JsonWorldStorage : IWorldStorage
    {
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            IncludeFields = true,
            WriteIndented = true
        };

        public void SaveMap(MapModel mapToSave, string filePath)
        {
            string jsonString = JsonSerializer.Serialize(mapToSave, _options);
            File.WriteAllText(filePath, jsonString);
        }

        public MapModel LoadMap(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Файл сохранения не найден!");

            string jsonString = File.ReadAllText(filePath);

            MapModel map = JsonSerializer.Deserialize<MapModel>(jsonString, _options)
                           ?? throw new InvalidDataException("Файл сохранения поврежден или имеет неверный формат!");

            return map;
        }
    }
}