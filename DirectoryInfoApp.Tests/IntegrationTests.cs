using DirectoryInfoApp.BL.Models;
using DirectoryInfoApp.BL.Providers;
using DirectoryInfoApp.BL.Services;
using System.Text.Json;

namespace DirectoryInfoApp.Tests
{
    /// <summary>
    /// Integration tests.
    /// </summary>
    [TestFixture, Category("IntegrationTests")]
    [Order(1)]
    public class IntegrationTests
    {
        [Test]
        public void FullIntegrationTest_ShouldLoadDirectoryGetUniqueExtensionsSerializeSaveJsonLoadJsonDeserializeSuccessfully()
        {
            // Arrange
            var fileNames = new string[] { "file1.txt", "file2.ini" };
            var fileExtensions = new string[] { ".txt", ".ini" };
            var subDirFileNames = new string[] { "file3.txt", "file4.txt" };
            var subDirFileExtensions = new string[] { ".txt", ".txt" };
            var expectedExtensions = new string[] { ".txt", ".ini" };
            var subDirectoryInfo = new DirectoryInfoModel("subDir")
            {
                Directories = { },
                Files = {
                    new FileInfoModel(subDirFileNames[0], subDirFileExtensions[0]),
                    new FileInfoModel(subDirFileNames[1], subDirFileExtensions[1]),
                }
            };
            var expectedDirectoryInfo = new DirectoryInfoModel("testDir")
            {
                Directories = { subDirectoryInfo },
                Files = {
                    new FileInfoModel(fileNames[0], fileExtensions[0]),
                    new FileInfoModel(fileNames[1], fileExtensions[1])
                }
            };
            var directoryPath = Directory.GetCurrentDirectory();
            directoryPath = Path.Combine(directoryPath, expectedDirectoryInfo.Name);
            var subDirectoryPath = Path.Combine(directoryPath, subDirectoryInfo.Name);

            var directory = Directory.CreateDirectory(directoryPath);
            Directory.CreateDirectory(subDirectoryPath);

            var file1Path = Path.Combine(directoryPath, fileNames[0]);
            var file2Path = Path.Combine(directoryPath, fileNames[1]);
            var file3Path = Path.Combine(subDirectoryPath, subDirFileNames[0]);
            var file4Path = Path.Combine(subDirectoryPath, subDirFileNames[1]);
            File.WriteAllText(file1Path, string.Empty);
            File.WriteAllText(file2Path, string.Empty);
            File.WriteAllText(file3Path, string.Empty);
            File.WriteAllText(file4Path, string.Empty);

            var directoryService = new DirectoryService(new DirectoryInfoProvider());
            var jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true // Ensure pretty printing for readability
            };
            var jsonService = new JsonService(jsonOptions);
            var jsonFilePath = "test.json";

            // Act
            var directoryInfoModel = directoryService.LoadDirectory(directoryPath);
            var actualExtensions = directoryService.GetUniqueExtensions(directoryInfoModel).ToList();

            var json = jsonService.SerializeDirectory(directoryInfoModel);
            jsonService.SaveJsonToFile(jsonFilePath, json);

            json = jsonService.LoadJsonFromFile(jsonFilePath);
            var deserealizedJson = jsonService.DeserializeDirectory(json);

            // Assert
            Assert.IsNotNull(deserealizedJson);
            Assert.That(deserealizedJson.Name, Is.EqualTo(expectedDirectoryInfo.Name));
            Assert.That(deserealizedJson.Files.Count, Is.EqualTo(expectedDirectoryInfo.Files.Count));
            Assert.That(deserealizedJson.Files[0].Name, Is.EqualTo(expectedDirectoryInfo.Files[0].Name));
            Assert.That(deserealizedJson.Files[1].Name, Is.EqualTo(expectedDirectoryInfo.Files[1].Name));
            Assert.That(deserealizedJson.Directories.Count, Is.EqualTo(expectedDirectoryInfo.Directories.Count));
            Assert.That(deserealizedJson.Directories[0].Name, Is.EqualTo(subDirectoryInfo.Name));
            Assert.That(deserealizedJson.Directories[0].Files.Count, Is.EqualTo(subDirectoryInfo.Files.Count));
            Assert.That(deserealizedJson.Directories[0].Files[0].Name, Is.EqualTo(subDirectoryInfo.Files[0].Name));
            Assert.That(deserealizedJson.Directories[0].Files[1].Name, Is.EqualTo(subDirectoryInfo.Files[1].Name));
            Assert.Contains(expectedExtensions[0], actualExtensions);
            Assert.Contains(expectedExtensions[1], actualExtensions);

            // Cleanup
            File.Delete(jsonFilePath);
            directory.Delete(true);
        }
    }
}