using DirectoryInfoApp.BL.Interfaces;
using DirectoryInfoApp.BL.Providers;

namespace DirectoryInfoApp.Tests.UnitTests.ProvidersTests
{
    /// <summary>
    /// Unit tests for the <see cref="DirectoryInfoProvider"/> class.
    /// </summary>
    [TestFixture, Category("UnitTests")]
    [Order(0)]
    public class DirectoryInfoProviderTests
    {
        IDirectoryInfoProvider _directoryInfoProvider;

        [SetUp]
        public void Setup()
        {
            _directoryInfoProvider = new DirectoryInfoProvider();
        }

        [Test]
        public void GetDirectoryInfo_ShouldReturnDirectoryInfo()
        {
            // Arrange
            var path = Directory.GetCurrentDirectory();

            // Act
            var result = _directoryInfoProvider.GetDirectoryInfo(path);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.FullName, Is.EqualTo(path));
        }

        [Test]
        public void GetDirectoryInfo_ShouldThrowArgumentNullExceptionForNullStrimgPath()
        {
            // Arrange
            string nullString = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _directoryInfoProvider.GetDirectoryInfo(nullString));
        }

        [Test]
        public void GetDirectoryInfo_ShouldThrowArgumentNullExceptionForEmptyStrimgPath()
        {
            // Arrange
            var emptyString = string.Empty;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _directoryInfoProvider.GetDirectoryInfo(string.Empty));
        }

        [Test]
        public void GetDirectoryInfo_ShouldThrowDirectoryNotFoundExceptionForInvalidPath()
        {
            // Arrange
            var invalidPath = "invalid_path";

            // Act & Assert
            Assert.Throws<DirectoryNotFoundException>(() => _directoryInfoProvider.GetDirectoryInfo(invalidPath));
        }
    }
}