using LineEditor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineEditor.Services
{
    public class ValidationService : IValidationService
    {
        public bool ValidateLineNumber(int lineNumber, int totalLines, bool forInsertion = false)
        {
            if (forInsertion)
            {
                // For insertion, line number should be between 1 and totalLines + 1
                return lineNumber >= 1 && lineNumber <= totalLines + 1;
            }
            else
            {
                // For deletion, line number should be between 1 and totalLines
                return lineNumber >= 1 && lineNumber <= totalLines;
            }
        }

        public bool ValidateLineText(string lineText)
        {
            // Line text cannot be null or empty
            return !string.IsNullOrWhiteSpace(lineText);
        }
    }

}
