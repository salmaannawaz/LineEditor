using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineEditor.Interfaces
{
    public interface IFileService
    {
        void LoadFile(string filePath);
        void SaveFile(string filePath);
        List<string> GetLines();
        void AddLine(string line, int index);
        void RemoveLine(int index);
    }

}
