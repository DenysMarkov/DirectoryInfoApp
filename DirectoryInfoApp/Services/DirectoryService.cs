using DirectoryInfoApp.BL.Interfaces;
using DirectoryInfoApp.BL.Models;

namespace DirectoryInfoApp.BL.Services
{
    /// <summary>
    /// Service for handling directory operations.
    /// </summary>
    public class DirectoryService : IDirectoryService
    {
        private readonly IDirectoryInfoProvider _directoryInfoProvider;

        public DirectoryService(IDirectoryInfoProvider directoryInfoProvider)
        {
            _directoryInfoProvider = directoryInfoProvider;
        }

        public DirectoryInfoModel LoadDirectory(string path)
        {
            // Validate the path
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("Invalid path.");
            }

            // Get the directory information using the provider
            var directoryInfo = _directoryInfoProvider.GetDirectoryInfo(path);

            // Load the directory structure
            var directoryModel = new DirectoryInfoModel(directoryInfo.Name);

            // Add files to directory model
            foreach (var file in _directoryInfoProvider.GetFiles(directoryInfo))
            {
                directoryModel.Files.Add(new FileInfoModel(file.Name, file.Extension));
            }

            // Add nested directories to directory model
            foreach (var directory in _directoryInfoProvider.GetDirectories(directoryInfo))
            {
                var subDirModel = LoadDirectory(directory.FullName);
                directoryModel.Directories.Add(subDirModel);
            }

            return directoryModel;
        }

        public HashSet<string> GetUniqueExtensions(IDirectoryInfoModel directory)
        {
            var extensions = new HashSet<string>();

            // Add file extensions to set
            foreach (var file in directory.Files)
            {
                extensions.Add(file.Extension);
            }

            // Recursively add extensions from subdirectories
            foreach (var subDirectory in directory.Directories)
            {
                var subDirectoryExtensions = GetUniqueExtensions(subDirectory);
                extensions.UnionWith(subDirectoryExtensions);
            }

            return extensions;
        }
    }
}
