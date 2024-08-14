using DirectoryInfoApp.BL.Interfaces;

namespace DirectoryInfoApp.BL.Providers
{
    /// <summary>
    /// Concrete implementation of IDirectoryInfoProvider that uses System.IO.DirectoryInfo.
    /// </summary>
    public class DirectoryInfoProvider : IDirectoryInfoProvider
    {
        public DirectoryInfo GetDirectoryInfo(string path)
        {
            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException($"Directory not found: {path}");
            }
            return new DirectoryInfo(path);
        }

        public FileInfo[] GetFiles(DirectoryInfo directoryInfo)
        {
            return directoryInfo.GetFiles();
        }

        public DirectoryInfo[] GetDirectories(DirectoryInfo directoryInfo)
        {
            return directoryInfo.GetDirectories();
        }
    }
}
