using DirectoryInfoApp.BL.Models;

namespace DirectoryInfoApp.BL.Interfaces
{
    /// <summary>
    /// Represents the contract for a directory's information.
    /// </summary>
    public interface IDirectoryInfoModel
    {
        /// <summary>
        /// Gets or sets the name of the directory.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the list of files in the directory.
        /// </summary>
        List<FileInfoModel> Files { get; set; }

        /// <summary>
        /// Gets or sets the list of nested directories within the directory.
        /// </summary>
        List<DirectoryInfoModel> Directories { get; set; }
    }
}
