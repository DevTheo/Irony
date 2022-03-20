using CommunityToolkit.Mvvm.ComponentModel;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Irony.UI.ViewModels.Models
{
    public partial class GrammarErrorRow : ObservableObject
    {
        [ObservableProperty]
        private string _level;

        [ObservableProperty]
        private string _message;

        [ObservableProperty]
        private ParserState _state;

        public GrammarErrorRow(string level, string message, ParserState state)
        {
            _level = level;
            _message = message;
            _state = state;
        }
    }
}
