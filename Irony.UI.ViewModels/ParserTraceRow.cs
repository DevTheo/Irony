using CommunityToolkit.Mvvm.ComponentModel;
using Irony.Parsing;

namespace Irony.UI.ViewModels
{
    public partial class ParserTraceRow : ObservableObject
    {
        [ObservableProperty]
        private ParserState _state;

        [ObservableProperty]
        private ParseTreeNode _stackTop;

        [ObservableProperty]
        private ParseTreeNode _input;

        [ObservableProperty]
        private string _message;

        [ObservableProperty]
        private System.Drawing.Color _foreColor = System.Drawing.Color.Black;

        public ParserTraceRow() { }
        public ParserTraceRow(ParserState state, ParseTreeNode stackTop, ParseTreeNode input, string message)
        {
            _state = state;
            _stackTop = stackTop;
            _input = input;
            _message = message;
        }

    }
}
