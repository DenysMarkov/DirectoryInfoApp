namespace DirectoryInfoApp.BL.Interfaces
{
    /// <summary>
    /// Provides an abstraction for accessing directory information.
    /// </summary>
    public interface IDirectoryInfoProvider
    {
        /// <summary>
        /// Gets a DirectoryInfo object for the specified path.
        /// </summary>
        /// <param name="path">The directory path.</param>
        /// <returns>The DirectoryInfo object.</returns>
        DirectoryInfo GetDirectoryInfo(string path);

        /// <summary>
        /// Retrieves an array of files in the specified directory.
        /// </summary>
        /// <param name="path">The path to the directory.</param>
        /// <returns>An array of <see cref="FileInfo"/> containing information about the files in the directory.</returns>
        public FileInfo[] GetFiles(DirectoryInfo directoryInfo);

        /// <summary>
        /// Retrieves an array of subdirectories in the specified directory.
        /// </summary>
        /// <param name="path">The path to the directory.</param>
        /// <returns>An array of <see cref="DirectoryInfo"/> containing information about the subdirectories.</returns>
        public DirectoryInfo[] GetDirectories(DirectoryInfo directoryInfo);
    }
}
