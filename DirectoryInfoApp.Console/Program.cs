using DirectoryInfoApp.BL.Services;
using DirectoryInfoApp.BL.Interfaces;
using DirectoryInfoApp.BL.Providers;
using System.Text.Json;

// Configure JSON serializer options
var jsonOptions = new JsonSerializerOptions
{
    WriteIndented = true // Pretty print JSON
};

IDirectoryService directoryService = new DirectoryService(new DirectoryInfoProvider());
IJsonService jsonService = new JsonService(jsonOptions);

Console.WriteLine("Input path to directory:");
var directoryPath = Console.ReadLine();

var directoryInfo = directoryService.LoadDirectory(directoryPath);

// Print all unique file extensions
var extensions = directoryService.GetUniqueExtensions(directoryInfo);
Console.WriteLine("Unique file extensions:");
foreach (var ext in extensions)
{
    Console.WriteLine(ext);
}

// Serialize to JSON
var json = jsonService.SerializeDirectory(directoryInfo);
Console.WriteLine("\nSerialized JSON:");
Console.WriteLine(json);

jsonService.SaveJsonToFile("directory_info.json", json);

// Deserialize JSON
var loadedJson = jsonService.LoadJsonFromFile("directory_info.json");
var deserializedDirectoryInfo = jsonService.DeserializeDirectory(loadedJson);
Console.WriteLine("\nDeserialized Directory Information:");
Console.WriteLine("Deserialized directory name: " + deserializedDirectoryInfo.Name);