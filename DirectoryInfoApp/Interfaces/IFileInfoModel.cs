namespace DirectoryInfoApp.BL.Interfaces
{
    /// <summary>
    /// Represents the contract for a file's information.
    /// </summary>
    public interface IFileInfoModel
    {
        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the file extension.
        /// </summary>
        string Extension { get; set; }
    }
}
