using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineEditor.Interfaces
{
    public interface IValidationService
    {
        bool ValidateLineNumber(int lineNumber, int totalLines, bool forInsertion = false);
        bool ValidateLineText(string lineText);
    }
}
