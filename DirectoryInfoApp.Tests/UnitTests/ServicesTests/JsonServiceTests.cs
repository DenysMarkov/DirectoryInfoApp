using DirectoryInfoApp.BL.Interfaces;
using DirectoryInfoApp.BL.Models;
using DirectoryInfoApp.BL.Services;
using Moq;
using System.Text.Json;

namespace DirectoryInfoApp.Tests.UnitTests.ServicesTests
{
    /// <summary>
    /// Unit tests for the <see cref="JsonService"/> class.
    /// </summary>
    [TestFixture, Category("UnitTests")]
    [Order(0)]
    public class JsonServiceTests
    {
        private IJsonService _jsonService;

        [SetUp]
        public void Setup()
        {
            var jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true // Ensure pretty printing for readability
            };
            _jsonService = new JsonService(jsonOptions);
        }

        [Test]
        public void SerializeDirectory_ShouldReturnValidJson()
        {
            // Arrange
            var directory = new Mock<IDirectoryInfoModel>();
            directory.SetupGet(d => d.Name).Returns("testDir");
            directory.SetupGet(d => d.Files).Returns(new List<FileInfoModel>
            {
                new FileInfoModel("file1.txt", ".txt")
            });
            directory.SetupGet(d => d.Directories).Returns(new List<DirectoryInfoModel>());

            // Act
            var json = _jsonService.SerializeDirectory(directory.Object);

            // Assert
            Assert.NotNull(json);
            Assert.IsTrue(json.Contains("\"Name\": \"testDir\""));
            Assert.IsTrue(json.Contains("\"Files\":"));
        }

        [Test]
        public void DeserializeDirectory_ShouldReturnValidObject()
        {
            // Arrange
            var json = "{\"Name\": \"testDir\",\"Files\": [{\"Name\": \"file1.txt\",\"Extension\": \".txt\"}],\"Directories\": []}";

            // Act
            var directory = _jsonService.DeserializeDirectory(json);

            // Assert
            Assert.NotNull(directory);
            Assert.That(directory.Name, Is.EqualTo("testDir"));
            Assert.That(directory.Files.Count, Is.EqualTo(1));
        }

        [Test]
        public void SaveJsonToFile_ShouldWriteToFile()
        {
            // Arrange
            var json = "{\"Name\": \"testDir\",\"Files\": [],\"Directories\": []}";
            var filePath = "test_output.json";

            // Act
            _jsonService.SaveJsonToFile(filePath, json);

            // Assert
            Assert.IsTrue(File.Exists(filePath));
            var content = File.ReadAllText(filePath);
            Assert.That(content, Is.EqualTo(json));

            // Cleanup
            File.Delete(filePath);
        }

        [Test]
        public void LoadJsonFromFile_ShouldReturnJsonString()
        {
            // Arrange
            var filePath = "test_output.json";
            var expectedJson = "{\"Name\": \"testDir\",\"Files\": [],\"Directories\": []}";
            File.WriteAllText(filePath, expectedJson);

            // Act
            var actualJson = _jsonService.LoadJsonFromFile(filePath);

            // Assert
            Assert.IsNotNull(actualJson);
            Assert.That(actualJson, Is.EqualTo(expectedJson));

            // Cleanup
            File.Delete(filePath);
        }

        [Test]
        public void LoadJsonFromFile_ShouldThrowFileNotFoundExceptionForNonExistentFile()
        {
            // Arrange
            var filePath = "non_existent_file.json";

            // Act & Assert
            Assert.Throws<FileNotFoundException>(() => _jsonService.LoadJsonFromFile(filePath));
        }

        [Test]
        public void DeserializeDirectory_ShouldThrowExceptionForInvalidJson()
        {
            // Arrange
            var invalidJson = "{\"Name\": \"testDir\",\"Files\": [{\"Name\": \"file1.txt\",\"Extension\": \".txt\"}";

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _jsonService.DeserializeDirectory(invalidJson));
        }
    }
}