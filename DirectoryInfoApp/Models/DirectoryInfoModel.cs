using DirectoryInfoApp.BL.Interfaces;

namespace DirectoryInfoApp.BL.Models
{
    /// <summary>
    /// Represents a directory's information, including its name, files, and nested directories.
    /// </summary>
    public class DirectoryInfoModel : IDirectoryInfoModel
    {
        /// <summary>
        /// Gets or sets the name of the directory.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the list of files in the directory.
        /// </summary>
        public List<FileInfoModel> Files { get; set; }

        /// <summary>
        /// Gets or sets the list of nested directories within the directory.
        /// </summary>
        public List<DirectoryInfoModel> Directories { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DirectoryInfoModel"/> class.
        /// </summary>
        /// <param name="name">The name of the directory.</param>
        public DirectoryInfoModel(string name)
        {
            Name = name;
            Files = new List<FileInfoModel>();
            Directories = new List<DirectoryInfoModel>();
        }
    }
}
