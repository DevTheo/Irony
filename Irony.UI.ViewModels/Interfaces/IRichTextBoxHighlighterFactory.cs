using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Irony.UI.ViewModels.Interfaces
{
    public interface IRichTextBoxHighlighterFactory
    {
        public IRichTextBoxHighlighter CreateRichTextBoxHighlighter(string dataName, LanguageData language);
    }

    public interface IRichTextBoxHighlighter : IDisposable
    {
        void AdapterActivate();
    }
}
