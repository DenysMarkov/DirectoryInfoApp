using DirectoryInfoApp.BL.Models;

namespace DirectoryInfoApp.BL.Interfaces
{
    /// <summary>
    /// Represents the contract for directory service operations.
    /// </summary>
    public interface IDirectoryService
    {
        /// <summary>
        /// Loads information about a directory and its contents.
        /// </summary>
        /// <param name="path">The path to the directory.</param>
        /// <returns>A directory model containing the loaded information.</returns>
        DirectoryInfoModel LoadDirectory(string path);

        /// <summary>
        /// Retrieves all unique file extensions in the directory and its subdirectories.
        /// </summary>
        /// <param name="directory">The directory model.</param>
        /// <returns>A set of unique file extensions.</returns>
        HashSet<string> GetUniqueExtensions(IDirectoryInfoModel directory);
    }
}
