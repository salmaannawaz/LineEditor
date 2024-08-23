using LineEditor.Interfaces;
using LineEditor.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

class Program
{
    static string filePath = @"c:\temp\myfile.txt";
    static void Main(string[] args)
    {
        if (!File.Exists(filePath))
        {
            string directoryPath = Path.GetDirectoryName(@"c:\temp\myfile.txt");
            Console.WriteLine($"No file found with the name 'myfile.txt' in {directoryPath}");
            return;
        }

        // Create a service collection and configure services
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        // Build the service provider
        var serviceProvider = serviceCollection.BuildServiceProvider();

        // Get the editor service and run it
        var editor = serviceProvider.GetService<IEditorService>();
        editor.Run();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        // Register services
        services.AddSingleton<IFileService>(provider => new FileService());
        services.AddSingleton<IValidationService>(provider => new ValidationService());
        services.AddSingleton<IEditorService>(provider => new EditorService(
            provider.GetService<IFileService>(),
            provider.GetService<IValidationService>(),
            filePath));
    }
}
