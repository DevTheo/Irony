using CommunityToolkit.Mvvm.ComponentModel;
using Irony.Parsing;
using System;

namespace Irony.UI.ViewModels
{
    public partial class CommonData : ObservableObject
    {
        public bool IsDesignMode { get; set; } = true;

        [ObservableProperty]
        private Grammar _LoadedGrammar;

        internal static void ApplicationDoEvents()
        {
            throw new NotImplementedException();
        }
    }
}
