namespace DirectoryInfoApp.BL.Interfaces
{
    /// <summary>
    /// Represents the contract for JSON service operations.
    /// </summary>
    public interface IJsonService
    {
        /// <summary>
        /// Serializes the directory information into JSON format.
        /// </summary>
        /// <param name="directory">The directory model to serialize.</param>
        /// <returns>A JSON string representing the directory information.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        string SerializeDirectory(IDirectoryInfoModel directory);

        /// <summary>
        /// Deserializes a JSON string back into a directory model.
        /// </summary>
        /// <param name="json">The JSON string representing the directory information.</param>
        /// <returns>The deserialized directory model.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        IDirectoryInfoModel DeserializeDirectory(string json);

        /// <summary>
        /// Saves the serialized JSON string to a file.
        /// </summary>
        /// <param name="path">The file path to save the JSON string.</param>
        /// <param name="json">The JSON string to save.</param>
        /// <exception cref="IOException"></exception>
        void SaveJsonToFile(string path, string json);

        /// <summary>
        /// Loads a JSON string from a file.
        /// </summary>
        /// <param name="path">The file path to load the JSON string from.</param>
        /// <returns>The loaded JSON string.</returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="IOException"></exception>
        string LoadJsonFromFile(string path);
    }
}
