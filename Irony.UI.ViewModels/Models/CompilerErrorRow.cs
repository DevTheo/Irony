using Irony.Parsing;

namespace Irony.UI.ViewModels.Models
{
    public readonly struct CompilerErrorRow
    {
        public readonly SourceLocation? Location;

        public readonly ParserState? ParserState;
        public readonly string? Message;

        public CompilerErrorRow(SourceLocation? location, string message, ParserState? parserState)
        {
            Location = location;
            Message = message;
            ParserState = parserState;
        }


        public CompilerErrorRow(SourceLocation location, ParserState parserState)
        {
            Location = location;
            Message = null;
            ParserState = parserState;
        }


    }
}
