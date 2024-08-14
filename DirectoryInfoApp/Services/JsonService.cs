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

        /// <summary>
        /// Serializes the directory information into JSON format.
        /// </summary>
        /// <param name="directory">The directory model to serialize.</param>
        /// <returns>A JSON string representing the directory information.</returns>
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

        /// <summary>
        /// Deserializes a JSON string back into a directory model.
        /// </summary>
        /// <param name="json">The JSON string representing the directory information.</param>
        /// <returns>The deserialized directory model.</returns>
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

        /// <summary>
        /// Saves the serialized JSON string to a file.
        /// </summary>
        /// <param name="path">The file path to save the JSON string.</param>
        /// <param name="json">The JSON string to save.</param>
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

        /// <summary>
        /// Loads a JSON string from a file.
        /// </summary>
        /// <param name="path">The file path to load the JSON string from.</param>
        /// <returns>The loaded JSON string.</returns>
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
