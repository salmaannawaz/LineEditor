using LineEditor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineEditor.Services
{
    public class EditorService : IEditorService
    {
        private readonly IFileService _fileService;
        private readonly IValidationService _validationService;
        private readonly string _filePath;

        public EditorService(IFileService fileService, IValidationService validationService, string filePath)
        {
            _fileService = fileService;
            _validationService = validationService;
            _filePath = filePath;
            _fileService.LoadFile(_filePath);
        }

        public void Run()
        {
            string command;
            do
            {
                Console.Write(">> ");
                command = Console.ReadLine();
                ExecuteCommand(command);
            }
            while (command != "quit");
        }

        private void ExecuteCommand(string command)
        {
            if (string.IsNullOrWhiteSpace(command))
            {
                Console.WriteLine("Command cannot be empty.");
                return;
            }

            var parts = command.Split(' ', 2);
            var cmd = parts[0].ToLower();

            switch (cmd)
            {
                case "list":
                    ListLines();
                    break;
                case "del":
                    DeleteLine(parts);
                    break;
                case "ins":
                    InsertLine(parts);
                    break;
                case "save":
                    _fileService.SaveFile(_filePath);
                    break;
                case "quit":
                    break;
                default:
                    Console.WriteLine("Unknown command.");
                    break;
            }
        }

        private void ListLines()
        {
            var lines = _fileService.GetLines();
            for (int i = 0; i < lines.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {lines[i]}");
            }
        }

        private void DeleteLine(string[] parts)
        {
            if (parts.Length < 2 || !int.TryParse(parts[1], out int lineNum) ||
                !_validationService.ValidateLineNumber(lineNum, _fileService.GetLines().Count))
            {
                Console.WriteLine("Invalid line number.");
                return;
            }

            _fileService.RemoveLine(lineNum - 1);
            Console.WriteLine($"Line {lineNum} deleted.");
        }

        private void InsertLine(string[] parts)
        {
            if (parts.Length < 2 || !int.TryParse(parts[1], out int lineNum) ||
                !_validationService.ValidateLineNumber(lineNum, _fileService.GetLines().Count, true))
            {
                Console.WriteLine("Invalid line number.");
                return;
            }

            Console.Write("Enter the new line text: ");
            var newLine = Console.ReadLine();
            if (!_validationService.ValidateLineText(newLine))
            {
                Console.WriteLine("Line text cannot be empty.");
                return;
            }

            _fileService.AddLine(newLine, lineNum - 1);
            Console.WriteLine($"Line inserted at position {lineNum}.");
        }
    }
}
