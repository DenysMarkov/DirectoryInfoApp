using DirectoryInfoApp.BL.Interfaces;

namespace DirectoryInfoApp.BL.Models
{
    /// <summary>
    /// Represents a file's information, including its name and extension.
    /// </summary>
    public class FileInfoModel : IFileInfoModel
    {
        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the file extension.
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileInfoModel"/> class.
        /// </summary>
        /// <param name="name">The name of the file.</param>
        /// <param name="extension">The file extension.</param>
        public FileInfoModel(string name, string extension)
        {
            Name = name;
            Extension = extension;
        }
    }
}
