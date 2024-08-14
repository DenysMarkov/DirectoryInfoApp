using DirectoryInfoApp.BL.Interfaces;
using DirectoryInfoApp.BL.Models;
using DirectoryInfoApp.BL.Services;
using Moq;

namespace DirectoryInfoApp.Tests.UnitTests.ServicesTests
{
    /// <summary>
    /// Unit tests for the <see cref="DirectoryService"/> class.
    /// </summary>
    [TestFixture, Category("UnitTests")]
    [Order(0)]
    public class DirectoryServiceTests
    {
        private Mock<IDirectoryInfoProvider> _directoryInfoProviderMock;
        private IDirectoryService _directoryService;

        [SetUp]
        public void Setup()
        {
            _directoryInfoProviderMock = new Mock<IDirectoryInfoProvider>();
            _directoryService = new DirectoryService(_directoryInfoProviderMock.Object);
        }

        [Test]
        public void LoadDirectory_ShouldLoadCorrectData()
        {
            // Arrange
            _directoryInfoProviderMock.Setup(x => x.GetDirectoryInfo(It.IsAny<string>()))
                                     .Returns(new DirectoryInfo("TestDirectory"));

            _directoryInfoProviderMock.Setup(p => p.GetFiles(It.IsAny<DirectoryInfo>()))
                        .Returns(new FileInfo[] { new FileInfo("file1.txt") });

            _directoryInfoProviderMock.Setup(p => p.GetDirectories(It.IsAny<DirectoryInfo>()))
                        .Returns(new DirectoryInfo[] { });

            // Act
            var directory = _directoryService.LoadDirectory("TestDirectory");

            // Assert
            Assert.NotNull(directory);
            Assert.That(directory.Name, Is.EqualTo("TestDirectory"));
            Assert.That(directory.Files.Count, Is.EqualTo(1));
            Assert.That(directory.Directories.Count, Is.EqualTo(0));
        }

        [Test]
        public void LoadDirectory_ShouldLoadNestedDirectoriesCorrectly()
        {

            // Arrange
            var rootDir = new DirectoryInfo("C:/rootDir");
            var nestedDir = new DirectoryInfo("C:/rootDir/nestedDir");

            _directoryInfoProviderMock.Setup(p => p.GetDirectoryInfo("C:/rootDir"))
                .Returns(rootDir);

            _directoryInfoProviderMock.Setup(p => p.GetFiles(rootDir))
                .Returns(new FileInfo[] { });

            _directoryInfoProviderMock.Setup(p => p.GetDirectories(rootDir))
                .Returns(new DirectoryInfo[] { nestedDir });

            _directoryInfoProviderMock.Setup(p => p.GetDirectoryInfo("C:\\rootDir\\nestedDir"))
                .Returns(nestedDir);

            _directoryInfoProviderMock.Setup(p => p.GetFiles(nestedDir))
                .Returns(new FileInfo[] { new FileInfo("C:/rootDir/nestedDir/file2.txt") });

            _directoryInfoProviderMock.Setup(p => p.GetDirectories(nestedDir))
                .Returns(new DirectoryInfo[] { });

            // Act
            var result = _directoryService.LoadDirectory("C:/rootDir");

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Name, Is.EqualTo("rootDir"));
            Assert.That(result.Directories.Count, Is.EqualTo(1));
            Assert.That(result.Directories[0].Name, Is.EqualTo("nestedDir"));
            Assert.That(result.Directories[0].Files.Count, Is.EqualTo(1));
            Assert.That(result.Directories[0].Files[0].Name, Is.EqualTo("file2.txt"));
        }

        [Test]
        public void LoadDirectory_ShouldHandleEmptyDirectory()
        {
            // Arrange
            var expectedNameDir = "emptyDir";
            var directoryInfo = new DirectoryInfo(expectedNameDir);
            _directoryInfoProviderMock.Setup(p => p.GetDirectoryInfo(expectedNameDir))
                .Returns(directoryInfo);

            // Act
            var actualDir = _directoryService.LoadDirectory(expectedNameDir);

            // Assert
            Assert.IsNotNull(actualDir);
            Assert.That(actualDir.Name, Is.EqualTo(expectedNameDir));
            Assert.IsEmpty(actualDir.Files);
            Assert.IsEmpty(actualDir.Directories);
        }

        [Test]
        public void GetUniqueExtensions_ShouldReturnUniqueExtensions()
        {
            // Arrange
            var directoryInfo = new DirectoryInfoModel("TestDirectory")
            {
                Files = new List<FileInfoModel>
                {
                    new FileInfoModel("file1.txt", ".txt"),
                    new FileInfoModel("file2.doc", ".doc"),
                    new FileInfoModel("file3.txt", ".txt")
                }
            };

            var service = new DirectoryService(_directoryInfoProviderMock.Object);

            // Act
            var extensions = service.GetUniqueExtensions(directoryInfo).ToList();

            // Assert
            Assert.That(extensions.Count, Is.EqualTo(2));
            Assert.Contains(".txt", extensions);
            Assert.Contains(".doc", extensions);
        }

        [Test]
        public void GetUniqueExtensions_ShouldHandleMultipleFileExtensions()
        {
            // Arrange
            var directoryInfo = new DirectoryInfoModel("TestDirectory")
            {
                Files = new List<FileInfoModel>
                {
                    new FileInfoModel("file1.txt", ".txt"),
                    new FileInfoModel("file2.cs", ".cs"),
                    new FileInfoModel("file3.jpg", ".jpg")
                }
            };

            // Act
            var result = _directoryService.GetUniqueExtensions(directoryInfo);

            // Assert
            Assert.That(result.Count(), Is.EqualTo(3));
            Assert.Contains(".txt", result.ToList());
            Assert.Contains(".cs", result.ToList());
            Assert.Contains(".jpg", result.ToList());
        }
    }
}