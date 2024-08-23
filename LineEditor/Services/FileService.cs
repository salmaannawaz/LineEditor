using LineEditor.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

public class FileService : IFileService
{
    private List<string> lines = new List<string>();

    public void LoadFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            lines = new List<string>(File.ReadAllLines(filePath));
        }
        else
        {
            lines = new List<string>();
            Console.WriteLine($"File not found: {filePath}");
        }
    }

    public void SaveFile(string filePath)
    {
        try
        {
            File.WriteAllLines(filePath, lines);
            Console.WriteLine($"Changes saved to: {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving file: {ex.Message}");
        }
    }

    public List<string> GetLines()
    {
        return lines;
    }

    public void AddLine(string line, int index)
    {
        lines.Insert(index, line);
    }

    public void RemoveLine(int index)
    {
        if (index >= 0 && index < lines.Count)
        {
            lines.RemoveAt(index);
        }
        else
        {
            Console.WriteLine("Invalid line index.");
        }
    }
}
