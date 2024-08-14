using System.Text.Json;
using DirectoryInfoApp.BL.Interfaces;
using DirectoryInfoApp.BL.Models;

namespace DirectoryInfoApp.BL.Services
{
    /// <summary>
    /// Service for handling JSON operations.
    /// </summary>
    public class JsonService : IJsonService
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public JsonService(JsonSerializerOptions jsonSerializerOptions)
        {
            _jsonSerializerOptions = jsonSerializerOptions;
        }

        public string SerializeDirectory(IDirectoryInfoModel directory)
        {
            try
            {
                return JsonSerializer.Serialize(directory, _jsonSerializerOptions);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to serialize directory info.", ex);
            }
        }

        public IDirectoryInfoModel DeserializeDirectory(string json)
        {
            try
            {
                return JsonSerializer.Deserialize<DirectoryInfoModel>(json, _jsonSerializerOptions);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to deserialize directory info.", ex);
            }
        }

        public void SaveJsonToFile(string path, string json)
        {
            try
            {
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                throw new IOException("Failed to save JSON to file.", ex);
            }
        }

        public string LoadJsonFromFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File not found.", path);
            }

            try
            {
                return File.ReadAllText(path);
            }
            catch (Exception ex)
            {
                throw new IOException("Failed to load JSON from file.", ex);
            }
        }
    }
}
