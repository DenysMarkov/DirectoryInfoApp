using DirectoryInfoApp.BL.Services;
using DirectoryInfoApp.BL.Interfaces;
using DirectoryInfoApp.BL.Providers;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

// Setup DI
var serviceProvider = new ServiceCollection()
    .AddSingleton<JsonSerializerOptions>(options =>
    {
        return new JsonSerializerOptions
        {
            WriteIndented = true,
        };
    })
    .AddSingleton<IDirectoryInfoProvider, DirectoryInfoProvider>()
    .AddSingleton<IDirectoryService, DirectoryService>()
    .AddSingleton<IJsonService, JsonService>()
    .BuildServiceProvider();

// Get services
var directoryService = serviceProvider.GetRequiredService<IDirectoryService>();
var jsonService = serviceProvider.GetRequiredService<IJsonService>();

// Input directory path
Console.WriteLine("Input path to directory:");
var directoryPath = Console.ReadLine();

try
{
    // Load directory info
    var directoryInfo = directoryService.LoadDirectory(directoryPath);

    // Print unique extensions
    var uniqueExtensions = directoryService.GetUniqueExtensions(directoryInfo);
    Console.WriteLine("Unique file extensions:");
    foreach (var ext in uniqueExtensions)
    {
        Console.WriteLine(ext);
    }

    // Serialize to JSON and save to file
    var json = jsonService.SerializeDirectory(directoryInfo);
    Console.WriteLine("\nSerialized JSON:");
    Console.WriteLine(json);
    jsonService.SaveJsonToFile("directoryInfo.json", json);

    // Deserialize JSON from file
    var loadedJson = jsonService.LoadJsonFromFile("directoryInfo.json");
    var deserializedDirectoryInfo = jsonService.DeserializeDirectory(loadedJson);
    Console.WriteLine("\nDeserialized Directory Information:");
    Console.WriteLine("Deserialized directory name: " + deserializedDirectoryInfo.Name);
    Console.WriteLine("Directory information successfully serialized and deserialized.");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}